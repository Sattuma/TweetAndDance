using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{

    public static GameModeManager instance;
    public GameMode activeGameMode;

    public bool level1Over;
    public bool level2Over;
    public bool level3Over;

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
        level1Over = true;
        //put this active when start to do level 2


        //activeGameMode = GameMode.level2;
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
