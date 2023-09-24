using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeManager : MonoBehaviour
{
    public delegate void GameAction();
    public static event GameAction GameLevelEnd;
    public static event GameAction BonusLevelEnd;
    public static event GameAction BonusLevelScore;
    public static event GameAction Success;
    public static event GameAction Fail;

    public delegate void LevelAction();
    public static event LevelAction NestCount;
    public static event LevelAction NestCountEnd;

    public static GameModeManager instance;

    public GameObject loadingScreenPrefab;

    [Header("Active Gamemode")]
    public GameMode activeGameMode;

    [Header("CurrentLevel")]
    public CurrentLevel currentLevel;

    [Header("Level State Booleans")]
    public bool levelActive;
    public bool isPaused;

    [Header("Level Timer")]
    public float timerLevel1;
    public float timerLevel2;
    public float timerLevel3;

    [Header("Secrets Found")]
    public int secretLevel1_1;
    public int secretLevel1_2;
    public int secretLevel1_3;

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

    public GameObject mouseMovementCheck;

    public enum GameMode
    {
        cutScene,
        gameLevel,
        bonusLevel
    }

    public enum CurrentLevel
    {
        Level1_1,
        Level1_2,
        Level1_3,
    }

    private void Awake()
    {
        if (instance != null)
        { Destroy(gameObject); }
        else
        { instance = this; DontDestroyOnLoad(instance);}

        activeGameMode = GameMode.gameLevel;
        levelActive = true;

    }

    //INVOKE EVENTS FUNCTIONS
    public void InvokeLevelCountOn()
    { NestCount?.Invoke();}
    public void InvokeLevelCountOff()
    { NestCountEnd?.Invoke();}
    public void InvokeSuccess()
    { Success?.Invoke(); }
    public void InvokeLevelFail()
    { Fail?.Invoke();}

    // ACTIVE GAMEMODE FUNCTIONS
    public void LevelActive()
    { activeGameMode = GameMode.gameLevel;}
    public void CutSceneActive()
    { activeGameMode = GameMode.cutScene;}
    public void BonusLevelActive()
    { activeGameMode = GameMode.bonusLevel;}

    // ACTIVE CURRENTLEVEL FUNCTIONS
    public void CurrentLevelActivation()
    {
        
    }

    public void AddScore(int score)
    {
        BonusLevelScore?.Invoke();
    }

    public void LevelTwoCleared()
    {
        Success?.Invoke();
        levelActive = false;
    }

    public void LevelThreeCleared()
    {
        Success?.Invoke();
        levelActive = false;
    }


    public void ChangeLevel(int levelIndex)
    {
        Instantiate(loadingScreenPrefab);
        GameObject.Find("LevelChanger(Clone)").GetComponent<ASync>().LoadLevel(levelIndex);
    }

}
