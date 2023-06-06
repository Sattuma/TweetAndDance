using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject startButton;
    public GameObject levelsButton;
    public GameObject settingButton;
    public GameObject quitButton;

    public void StartGame(int index)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index);

        GameModeManager.instance.level1Over = false;
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
