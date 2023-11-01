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
    public static event GameAction GameLevelEnd;
    public static event GameAction BonusLevelEnd;
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

    [Header("Level Change String Array")]
    public string[] levelName;
    public string[] bonusLevelName;

    [Header("Level State Booleans")]
    public bool cutsceneActive;
    public bool levelActive;
    public bool isPaused;
    public bool cannotResumeFromPause;

    [Header("Level Timer")]
    public float timerLevel1;
    public float timerLevel2;
    public float timerLevel3;

    [Header("PickUp Amount")]
    public int leafCount;
    public int stickCount;
    public int birchStickCount;
    public int strawberryCount;
    public int blossomCount;
    public int dandelionCount;

    [Header("PickUp Points")]
    public int leafPoints;
    public int stickPoints;
    public int birchStickPoints;
    public int strawberryPoints;
    public int blossomPoints;
    public int dandelionPoints;

    [Header("Level HighScores")]
    public int highScoreLevel1_1;
    public int highScoreLevel1_2;
    public int highScoreLevel1_3;

    [Header("Secrets Found/missed")]
    public int secretLevel1_1;
    public int secretLevel1_2;
    public int secretLevel1_3;

    [Header("Variables on level")]
    public int levelScore;
    public int secretsMissed;

    public GameObject mouseMovementCheck;

    public int levelIndex;

    public enum GameMode
    {
        mainMenu,
        cutScene,
        gameLevel,
        bonusLevel
    }

    public enum CurrentLevel
    {
        MainMenu,
        Level1_1 = 1,
        Level1_2,
        Level1_3,
    }

    private void Awake()
    {
        if (instance != null)
        { Destroy(gameObject); }
        else
        { instance = this; DontDestroyOnLoad(instance); }

        activeGameMode = GameMode.mainMenu;
    }

    private void Start()
    {
        SetData();
        // kaikki GetData otetaan levelchangerisa jo valmiiksi josta info pisteistä etc gamemodemanageriin
    }

    //----------------------------------------


    //INVOKE EVENTS FUNCTIONS
    public void StartLevelInvoke()
    { StartLevel?.Invoke(); }
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



    //----------------------------------------


    // ACTIVE GAMEMODE FUNCTIONS
    public void MainMenuActive()
    { activeGameMode = GameMode.mainMenu; }
    public void LevelActive()
    { activeGameMode = GameMode.gameLevel;}
    public void CutSceneActive()
    { activeGameMode = GameMode.cutScene;}
    public void BonusLevelActive()
    { activeGameMode = GameMode.bonusLevel;}

    // ACTIVE CURRENTLEVEL FUNCTIONS
    public void ActivateCurrentLevel(string levelName)
    {
        currentLevel = (CurrentLevel)System.Enum.Parse(typeof(CurrentLevel), levelName);
    }
    public void ActivateLevel1_1()
    { currentLevel = CurrentLevel.Level1_1;  }
    public void ActivateLevel1_2()
    { currentLevel = CurrentLevel.Level1_2; }
    public void ActivateLevel1_3()
    { currentLevel = CurrentLevel.Level1_3; }

    public void AddScore(int score)
    {
        BonusLevelScore?.Invoke();
    }

    //----------------------------------------


    public void ChangeLevel(string levelName)
    {
        Instantiate(loadingScreenPrefab);
        GameObject.Find("LevelChanger(Clone)").GetComponent<ASync>().LoadLevel(levelName);
    }
    public void SetData()
    {
        DataManager.instance.SetLevelTimers(timerLevel1, timerLevel2, timerLevel3);
    }

    public void ResetEvents()
    {
        StartLevel = null;
        GameLevelEnd = null;
        BonusLevelEnd = null;
        PauseOn = null;
        BonusLevelScore = null;
        Success = null;
        Fail = null;
        NestCount = null;
        NestCountEnd = null;
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

    public void SecretsCheck(GameObject[] array)
    {
        if (currentLevel == CurrentLevel.Level1_1)
        {
            for (int i = 0; i < array.Length; i++)
            {
                i = array.Length;
                secretLevel1_1 = i;
                DataManager.instance.SetLevelSecrets(secretLevel1_1);
            }
        }

        if (currentLevel == CurrentLevel.Level1_2)
        {
            for (int i = 0; i < array.Length; i++)
            {
                i = array.Length;
                secretLevel1_2 = i;
                DataManager.instance.SetLevelSecrets(secretLevel1_2);
            }
        }

        if (currentLevel == CurrentLevel.Level1_3)
        {
            for (int i = 0; i < array.Length; i++)
            {
                i = array.Length;
                secretLevel1_3 = i;
                DataManager.instance.SetLevelSecrets(secretLevel1_3);
            }
        }

    }

    //----------------------------------------

    private void OnDestroy()
    {
        DataManager.instance.SetLevelAudio(1, 1, 1);
    }

}
