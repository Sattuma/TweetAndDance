using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public delegate void GameAction();
    public static event GameAction Level2Score;
    public static event GameAction Level2End;
    public static event GameAction Success;
    public static event GameAction Fail;

    public static GameModeManager instance;

    public GameMode activeGameMode;

    public bool levelActive;



    public int scoreLevel2 = 0;
    public int scoreEndCount = 0;
    public int scoreLevel3 = 0;

    public enum GameMode
    {
        level1,
        level2,
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
        activeGameMode = GameMode.level3;
    }

    public void LevelThreeCleared()
    {
        levelActive = false;
    }

    public void LevelFailed()
    {
        Fail?.Invoke();
    }

}
