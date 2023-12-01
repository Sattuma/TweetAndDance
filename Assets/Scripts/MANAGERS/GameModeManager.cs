using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameModeManager : MonoBehaviour
{
    //GAMEACTION EVENTS
    public delegate void GameAction();

    public static event GameAction ControllerCheck;

    public static event GameAction RewardLevel;
    public static event GameAction StartLevel;
    public static event GameAction StartLevelCountOver;
    public static event GameAction SecretCountForMenu;
    public static event GameAction LevelActiveOn;
    public static event GameAction PauseOn;
    public static event GameAction Success;
    public static event GameAction Fail;
   
    //LEVELACTION EVENTS
    public delegate void LevelAction();
    public static event LevelAction NestCount;
    public static event LevelAction NestCountEnd;

    //BONUSACTION EVENTS
    public delegate void BonusAction();
    public static event BonusAction BonusOneStart;
    public static event BonusAction BonusOneEnd;
    public static event BonusAction BonusMeterAnimOn;
    public static event BonusAction BonusMeterAnimOff;
    public static event BonusAction BonusSuccess;
    public static event BonusAction BonusFail;


    //INSTANCE TO SELF
    public static GameModeManager instance;

    //LOADING SCREEN PREFAB
    public GameObject loadingScreenPrefab;

    [Header("Game Difficulty")]
    public Difficulty difficulty;

    [Header("Active Gamemode")]
    public GameMode activeGameMode;

    [Header("CurrentLevel")]
    public CurrentLevel currentLevel;

    public int levelIndex;

    [Header("Level Change String Array")]
    public string[] levelName;
    public string[] bonusLevelName;
    public string[] endScreenName;

    [Header("Level State Booleans")]
    public bool cutsceneActive;
    public bool levelActive;
    public bool isPaused;
    public bool cannotResumeFromPause;

    [Header("BonusLevel State Booleans")]
    public bool rewardClaimed;
    public bool bonusLevelActive;
    public bool bonusLevelEnd;

    [Header("Level Timer")]
    public float timerNormalMode;
    public float timerHardMode;

    [Header("PickUp Amount")] // ei vielä käytössä - testing
    public int leafCount;
    public int stickCount;
    public int birchStickCount;
    public int strawberryCount;
    public int blossomCount;
    public int dandelionCount;

    [Header("PickUp Points")] // ei vielä käytössä -testing
    public int leafPoints;
    public int stickPoints;
    public int birchStickPoints;
    public int strawberryPoints;
    public int blossomPoints;
    public int dandelionPoints;

    [Header("Level HighScores")]
    public int[] levelHighScores; //?? tämä samalla tavalla kun secretit
    public int highScoreLevel1_1; // turhia?
    public int highScoreLevel1_2; // turhia?
    public int highScoreLevel1_3; // turhia?

    [Header("Secrets Found/missed")]
    public int[] secretInLevel;
    public int[] secretMissedInLevel;

    [Header("Variables on Main level")]
    // main level score which is saved
    public int levelScore; //?? tämä samalla tavalla kun secretit
    //tracking on level which is temporary and trigger functions - no high score or saving - reset with OnDestroy on levelscripts
    //MAIN LEVEL
    public int secretCurrentForMenu;
    public int secretTotalForMenu;
    public int secretFoundTemp;
    public int secretMissedTemp;
    public int secretTotalTemp;

    [Header("Variables on bonus One")]
    public float bonusOnelevelScoreTemp;
    public float bonusOneSongTimeTotalTemp;
    public float bonusOneSongTimeTemp;


    public GameObject mouseMovementCheck;


    public enum Difficulty
    {
        Normal,
        Hard
    }
    //Handles functions as player movement, pause etc..
    public enum GameMode
    {
        mainMenu,
        cutScene,
        gameLevel,
        bonusLevel
    }

    //Handles scoring, secrets, info etc..
    public enum CurrentLevel
    {
        MainMenu,
        Level1_1,
        Level1_2,
        Level1_3,
        Bonus1,
        Bonus2,
        Bonus3
    }

    private void Awake()
    {
        if (instance != null)
        { Destroy(gameObject); }
        else
        { instance = this; DontDestroyOnLoad(instance); }

        StartValues();


    }

    public void StartValues()
    {
        //WHEN GAME IS STARTED VALUES HERE
        activeGameMode = GameMode.mainMenu;
        currentLevel = CurrentLevel.MainMenu;
        levelIndex = 0;
        difficulty = Difficulty.Normal;
        DataManager.instance.GetLevelSecrets();
        DataManager.instance.SetLevelTimers(timerNormalMode, timerHardMode);
    }

    //INVOKE EVENTS FUNCTIONS
    public void ControllerCheckInvoke()
    { ControllerCheck?.Invoke(); }
    public void RewardLevelInvoke()
    { RewardLevel?.Invoke(); }
    public void StartLevelInvoke()
    { StartLevel?.Invoke(); }
    public void StartCountOverInvoke()
    { StartLevelCountOver?.Invoke(); }
    public void InvokeSecretCountForMenu()
    { SecretCountForMenu?.Invoke(); }
    public void InvokeLevelActiveOn()
    { LevelActiveOn?.Invoke(); }
    public void InvokeLevelCountOn()
    { NestCount?.Invoke();}
    public void InvokeLevelCountOff()
    { NestCountEnd?.Invoke();}
    public void InvokePause()
    { PauseOn?.Invoke(); }
    public void InvokeSuccess()
    { Success?.Invoke(); }
    public void InvokeLevelFail()
    { Fail?.Invoke();}
    public void InvokeBonusOneStart()
    { BonusOneStart?.Invoke();  }
    public void InvokeBonusOneEnd()
    { BonusOneEnd?.Invoke(); }
    public void InvokeBonusMeterAnimOn()
    { BonusMeterAnimOn?.Invoke(); }
    public void InvokeBonusMeterAnimOff()
    { BonusMeterAnimOff?.Invoke(); }
    public void InvokeBonusSuccess()
    { BonusSuccess?.Invoke(); rewardClaimed = true; }
    public void InvokeBonusFail()
    { BonusFail?.Invoke(); }

    // ACTIVATE GAMEMODE FUNCTIONS
    public void MainMenuActive()
    { 
        activeGameMode = GameMode.mainMenu; 
    }
    public void LevelActive()
    { 
        activeGameMode = GameMode.gameLevel;
        levelActive = true;
        cutsceneActive = false;
    }
    public void CutSceneActive()
    { 
        activeGameMode = GameMode.cutScene;
        levelActive = false;
        cutsceneActive = true;
    }
    public void BonusLevelActive()
    { 
        activeGameMode = GameMode.bonusLevel;
        levelActive = true;
        cutsceneActive = false;
    }

    // ACTIVATE CURRENTLEVEL FUNCTIONS
    public void ActivateCurrentLevel(string levelName) // VAIHDETAAN KENTÄN NIMI
    {
        currentLevel = (CurrentLevel)System.Enum.Parse(typeof(CurrentLevel), levelName);
    }

    public void ActivateNextLevel()
    {
        levelIndex += 1; 
        ChangeLevel(levelName[levelIndex]);
    }
    public void ActivateBonusLevel()
    {
        if (levelIndex <= 2 && levelIndex > 0)
        { ChangeLevel(bonusLevelName[1]);}
        if (levelIndex <= 5 && levelIndex > 3)
        { ChangeLevel(bonusLevelName[2]);}
        if (levelIndex <= 8 && levelIndex > 6)
        { ChangeLevel(bonusLevelName[3]);}
    }

    public void ChangeLevel(string levelName)
    {
        Instantiate(loadingScreenPrefab);
        GameObject.Find("LevelChanger(Clone)").GetComponent<ASync>().LoadLevel(levelName);
    }

    public void AddBonusScore(int score)
    {
        bonusOnelevelScoreTemp += score;
    }
    public void AddLevelScore(int score)
    {
        //level score invoke jos tarvii
    }

    public void HighScoreCheck()
    {
        if (currentLevel == CurrentLevel.Level1_1)
        {
            if (levelScore > highScoreLevel1_1)
            { DataManager.instance.SetLevelPoints(levelScore); }
        }

        if (currentLevel == CurrentLevel.Level1_2)
        {
            if (levelScore > highScoreLevel1_2)
            { DataManager.instance.SetLevelPoints(levelScore); }
        }

        if (currentLevel == CurrentLevel.Level1_3)
        {
            if (levelScore > highScoreLevel1_3)
            { DataManager.instance.SetLevelPoints(levelScore); }
        }
    }

    public void SecretsCheck(int secretsFound, int secretsMissed)
    {
        secretInLevel[levelIndex] = secretsFound;
        secretMissedInLevel[levelIndex] = secretsMissed;
        DataManager.instance.SetLevelSecrets(secretsFound);
    }

    public void CheckBonusLevelAccess()
    {
        if (secretMissedInLevel[levelIndex] == 0)
        { ActivateBonusLevel();}
        else { ActivateNextLevel(); }
    }
    //----------------------------------------

    public void AddTime(int value)
    {
        timerNormalMode += value;
        timerHardMode += value;

        if(timerNormalMode >= PlayerPrefs.GetFloat("TimerNormal"))
        {
            timerNormalMode = PlayerPrefs.GetFloat("TimerNormal");
        }
        if(timerHardMode >= PlayerPrefs.GetFloat("TimerHard"))
        {
            timerHardMode = PlayerPrefs.GetFloat("TimerHard");
        }


    }

    public void DefaultValues()
    {
        activeGameMode = GameMode.mainMenu;
        currentLevel = CurrentLevel.MainMenu;
        levelIndex = 0;
    }

    //nämä restoidaan aina levelChangerissa eli Async scriptissä
    public void ResetEvents()
    {
        //GAMEACTION RESET
        ControllerCheck = null;
        RewardLevel = null;
        StartLevel = null;
        StartLevelCountOver = null;
        LevelActiveOn = null;
        SecretCountForMenu = null;
        PauseOn = null;
        Success = null;
        Fail = null;

        //LEVELACTION RESET
        NestCount = null;
        NestCountEnd = null;

        //LEVELACTION RESET
        BonusOneStart = null;
        BonusOneEnd = null;
        BonusMeterAnimOn = null;
        BonusMeterAnimOff = null;
        BonusSuccess = null;
        BonusFail = null;
    }

}
