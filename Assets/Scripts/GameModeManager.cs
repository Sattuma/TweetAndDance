using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{

    public static GameModeManager instance;
    public GameMode activeGameMode;

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

    public void LevelOneCleared()
    {
        activeGameMode = GameMode.level2;
    }

    public void AddScore()
    {
        //kenttien Scoret
    }

    public void LevelFailed()
    {
        //pelaaja fail anim
        //level pisteet
    }

}
