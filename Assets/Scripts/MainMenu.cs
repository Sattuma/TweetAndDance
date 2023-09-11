using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("MainMenu")]
    public GameObject startButton;
    public GameObject settingButton;
    public GameObject howToPlayButton;
    public GameObject creditsButton;
    public GameObject backButton;
    public GameObject quitButton;



    private void Start()
    {
        Time.timeScale = 1;
        AudioManager.instance.PlayMusicFX(0);
    }

    public void ContinueGame()
    {
        Debug.Log("Continue Game Clicked");
    }

    public void StartGame(string levelIndex)
    {
        AudioManager.instance.musicSource.Stop();
        //GameModeManager.instance.levelActive = false;
        //GameModeManager.instance.activeGameMode = GameModeManager.GameMode.cutScene;
        SceneManager.LoadScene(levelIndex);
    }

    public void OpenSettings()
    {
        Debug.Log("Settings opened");
    }

    public void OpenHowToPlay()
    {
        Debug.Log("HowToPlay Opened");
    }

    public void OpenCredits()
    {
        Debug.Log("Credits Opened");
    }

    public void OpenBack()
    {
        Debug.Log("Back Button Clicked");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Button Clicked");
        Application.Quit();
    }
}
