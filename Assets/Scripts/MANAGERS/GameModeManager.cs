using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameModeManager : MonoBehaviour
{
    //GAMEACTION EVENTS
    public delegate void GameAction();
    public static event GameAction StartLevel;
    public static event GameAction StartLevelCountOver;
    public static event GameAction PauseOn;
    public static event GameAction BonusLevelScore;
    public static event GameAction Success;
    public static event GameAction Fail;
   
    //LEVELACTION EVENTS
    public delegate void LevelAction();
    public static event LevelAction NestCount;
    public static event LevelAction NestCountEnd;


    //INSTANCE TO SELF
    public static GameModeManager instance;

    //LOADING SCREEN PREFAB
    public GameObject loadingScreenPrefab;

    [Header("Active Gamemode")]
    public GameMode activeGameMode;

    [Header("CurrentLevel")]
    public CurrentLevel currentLevel;

    public int levelIndex;

    [Header("Level Change String Array")]
    public string[] levelName;
    public string[] bonusLevelName;

    [Header("Level State Booleans")]
    public bool rewardClaimed;
    public bool cutsceneActive;
    public bool levelActive;
    public bool bonusLevelActive;
    public bool isPaused;
    public bool cannotResumeFromPause;

    [Header("Level Timer")]
    public float timerLevel1;
    public float timerLevel2;
    public float timerLevel3;

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

    [Header("Variables on level")]
    public int levelScore; //?? tämä samalla tavalla kun secretit
    public int bonuslevelScore; //?? tämä samalla tavalla kun secretit

    public int secretFoundTemp;
    public int secretMissedTemp;
    public int secretTotalTemp;

    public GameObject mouseMovementCheck;

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

        //when game open - first scene
        activeGameMode = GameMode.mainMenu;

    }

    private void Start()
    {
        GetData(); // päälle aina, testain vuoksi pois
        SetData(); // pääle ain testiun vuoksi pois
        // kaikki GetData otetaan levelchangerisa jo valmiiksi josta info pisteistä etc gamemodemanageriin -
        // (ADD) why the fuck? hmm mietitätä vielä
    }

    //INVOKE EVENTS FUNCTIONS
    public void StartLevelInvoke()
    { StartLevel?.Invoke(); }
    public void StartCountOverInvoke()
    { StartLevelCountOver?.Invoke(); }
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
        if (levelIndex <= 3 && levelIndex > 0)
        { ChangeLevel(bonusLevelName[1]);}
        if (levelIndex <= 6 && levelIndex > 3)
        { ChangeLevel(bonusLevelName[2]);}
        if (levelIndex <= 9 && levelIndex > 6)
        { ChangeLevel(bonusLevelName[3]);}
    }

    public void ChangeLevel(string levelName)
    {
        Instantiate(loadingScreenPrefab);
        GameObject.Find("LevelChanger(Clone)").GetComponent<ASync>().LoadLevel(levelName);
    }
    public void SetData()
    {
        DataManager.instance.SetLevelTimers(timerLevel1, timerLevel2, timerLevel3);
    }

    public void AddBonusScore(int score)
    {
        BonusLevelScore?.Invoke();
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

    public void ResetEvents()
    {
        //GAMEACTION RESET
        StartLevel = null;
        StartLevelCountOver = null;
        PauseOn = null;
        BonusLevelScore = null;
        Success = null;
        Fail = null;

        //LEVELACTION RESET
        NestCount = null;
        NestCountEnd = null;
    }

    public void GetData()
    {
        DataManager.instance.GetLevelSecrets();
    }

}
