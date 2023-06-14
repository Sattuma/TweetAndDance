using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("MainMenu")]
    public GameObject startButton;
    public GameObject levelsButton;
    public GameObject settingButton;
    public GameObject quitButton;

    public void StartGame(string levelIndex)
    {
        GameModeManager.instance.levelActive = false;
        GameModeManager.instance.activeGameMode = GameModeManager.GameMode.cutScene1;
        SceneManager.LoadScene(levelIndex);
    }

    public void OpenLevels()
    {
        Debug.Log("levels opened");
    }

    public void OpenSettings()
    {
        Debug.Log("settings opened");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
