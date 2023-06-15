using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public delegate void GameAction();
    public static event GameAction Level2Score;
    public static event GameAction Level1End;
    public static event GameAction Level2End;
    //public static event GameAction Level3End;
    public static event GameAction Success;
    public static event GameAction Fail;

    public static GameModeManager instance;

    public GameMode activeGameMode;

    public bool levelActive;

    public bool level2Retry;

    public float timerLevel1;
    public int scoreLevel2 = 0;
    public int scoreEndCount = 0;
    public int scoreLevel3 = 0;

    public enum GameMode
    {
        cutScene1,
        level1,
        cutScene2,
        level2,
        cutScene3,
        level3
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    //TÄHÄN JOKU PAREMPI SYSTEEMI SAISKO UPDATESTA CHECKIN POIS JA MUUTA KAUTTA?
    private void Update()
    {
        if (scoreEndCount >= 5)
        {
            levelActive = false;
            Level2End?.Invoke();
        }
    }

    public void InvokeLevel1End()
    {
        Level1End?.Invoke();
    }

    public void CutScene1Active()
    {
        activeGameMode = GameMode.cutScene1;
    }
    public void Level1Active()
    {
        activeGameMode = GameMode.level1;
    }
    public void CutScene2Active()
    {
        activeGameMode = GameMode.cutScene2;
    }
    public void Level2Active()
    {
        activeGameMode = GameMode.level2;
    }
    public void CutScene3Active()
    {
        activeGameMode = GameMode.cutScene3;
    }
    public void Level3Active()
    {
        activeGameMode = GameMode.level3;
    }

    public void LevelOneCleared()
    {
        Success?.Invoke();
    }
    
    public void AddScore(int score)
    {
        scoreLevel2 += score;
        Level2Score?.Invoke();
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

    public void LevelFailed()
    {
        Fail?.Invoke();
    }

}
