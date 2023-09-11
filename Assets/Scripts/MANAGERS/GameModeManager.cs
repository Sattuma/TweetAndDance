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

    public static GameModeManager instance;

    public GameMode activeGameMode;

    public bool levelActive;
    public bool isPaused;

    public float timerLevel1;


    public enum GameMode
    {
        cutScene,
        gameLevel,
        bonusLevel
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

    public void InvokeLevelFail()
    {
        Fail?.Invoke();
    }

    public void CutScene1Active()
    {
        activeGameMode = GameMode.cutScene;
    }
    public void Level1Active()
    {
        activeGameMode = GameMode.gameLevel;
    }
    public void CutSceneActive()
    {
        activeGameMode = GameMode.cutScene;
    }
    public void BonusLevel()
    {
        activeGameMode = GameMode.bonusLevel;
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

    public void LevelFailed()
    {
        Fail?.Invoke();
    }

}
