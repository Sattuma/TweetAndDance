using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    /*
    public delegate void GameActions();
    public static event GameActions GameEnd;
    public static event GameActions GameModeSelect;
    */

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
            DontDestroyOnLoad(instance); // not destroyed between scenes
        }
    }

    public void EndGameFunction()
    {
        Debug.Log("KENTTÄ LÄPI");
        activeGameMode = GameMode.level2;
    }

}
