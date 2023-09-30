using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{

    [Header("UI Timer HUDvariables")]
    public GameObject timerText;
    public GameObject timerCountText;

    [Header("UI points HUDvariables")]
    public GameObject pointsText;
    public GameObject pointsCountText;

    [Header("UI Beginning Texts")]
    public GameObject[] beginning = new GameObject[2];

    [Header("UI OnScreen CountDown")]
    public GameObject[] countDown = new GameObject[3];

    [Header("UI Menus & Buttons")]

    public GameObject cutSceneDemoButton;//poista kun oikea tehd‰‰n

    public GameObject pauseButton;
    public GameObject backButton;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject successMenu;
    public GameObject gameOverMenu;

    [Header("UI Sliders")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;

    private void Awake()
    {
        GetData();
    }
    private void Start()
    {
        //LEVEL EVENT CALL FOR FUNCTIONS
        GameModeManager.StartLevel += ActivateLevel;

        //GAME EVENT CALL FOR FUNCTIONS
        GameModeManager.PauseOn += PauseMenu;
        GameModeManager.Success += SuccessMenuOnLevel;
        GameModeManager.Fail += FailedMenuOnLevel;

        GameModeManager.NestCount += StartLevelEndCount;
        GameModeManager.NestCountEnd += CancelLevelEndCount;

        beginning[0].SetActive(false);
        beginning[1].SetActive(false);

    }

    public void OnPointerEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    private void ActivateLevel()
    {
        StartCoroutine(ActivateLevelCoRoutine());

    }
    IEnumerator ActivateLevelCoRoutine()
    {

        yield return new WaitForSecondsRealtime(1f);
        beginning[0].SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        beginning[0].SetActive(false);
        beginning[1].SetActive(true);
        GameModeManager.instance.levelActive = true;
        yield return new WaitForSecondsRealtime(0.5f);
        beginning[1].SetActive(false);
    }


    //----------------------------------------------


    //COUNTDOWN START FOR LEVEL END
    public void StartLevelEndCount()
    { StartCoroutine(LevelEndCount()); }

    public IEnumerator LevelEndCount()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        countDown[0].SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        countDown[0].SetActive(false);
        countDown[1].SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        countDown[1].SetActive(false);
        countDown[2].SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        countDown[2].SetActive(false);
        GameModeManager.instance.InvokeSuccess();
    }
    //COUNTDOWN CANCEL FOR LEVEL END
    public void CancelLevelEndCount()
    {
        StopAllCoroutines();
        countDown[0].SetActive(false);
        countDown[1].SetActive(false);
        countDown[2].SetActive(false);
    }
    //----------------------------------------------

    //GAME LEVEL ENDING MENU WINDOWS
    public void SuccessMenuOnLevel()
    {
        GameModeManager.instance.levelActive = false;
        successMenu.SetActive(true);
    }
    public void FailedMenuOnLevel()
    {
        GameModeManager.instance.levelActive = false;
        gameOverMenu.SetActive(true);
    }
    //------------------------------------------------
    //PAUSE MENU WINDOW FUNCTIONS
    public void PauseMenu()
    {
        if (Time.timeScale == 1)
        {
            GameModeManager.instance.isPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            GameModeManager.instance.isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        backButton.SetActive(true);
    }
    public void BackButtonPause()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        backButton.SetActive(false);
    }
    //------------------------------------------------




    //LEVEL2 - EVENT CALLS
    public void UpdateScore()
    {
        //pointsCountText.GetComponent<TextMeshProUGUI>().text = GameModeManager.instance.scoreLevel2.ToString() + "/ 2000";
    }



    //----------------------------------------------



    // BUTTON CALLS FOR NAVIGATION BETWEEN MENUS AND SCENES
    public void Continue()
    {


    }

    public void Retry()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
        {
            StopAllCoroutines();
            if (GameModeManager.instance.isPaused)
            {
                PauseMenu();
            }
        }

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel)
        {
            StopAllCoroutines();
            if (GameModeManager.instance.isPaused)
            {
                PauseMenu();
            }
        }
    }

    public void CutSceneDemo()//poista kun oikea tehd‰‰n
    {
        cutSceneDemoButton.SetActive(false);
        GameModeManager.instance.cutsceneActive = false;

    }

    public void ToMainMenu(int sceneIndex)
    {
        SetData();
        GameModeManager.instance.ChangeLevel(sceneIndex);
    }

    public void GetData()
    {
        masterVolumeSlider.value = AudioManager.instance.masterVolumeValue;
        effectsVolumeSlider.value = AudioManager.instance.effectsVolumeValue;
        musicVolumeSlider.value = AudioManager.instance.musicVolumeValue;
        DataManager.instance.GetLevelAudio();
    }
    public void SetData()
    {
        AudioManager.instance.masterVolumeValue = masterVolumeSlider.value;
        AudioManager.instance.effectsVolumeValue = effectsVolumeSlider.value;
        AudioManager.instance.musicVolumeValue = musicVolumeSlider.value;

        DataManager.instance.SetLevelAudio(masterVolumeSlider.value, effectsVolumeSlider.value, musicVolumeSlider.value);
    }

    //----------------------------------------------

    public void OnDestroy()
    {
        /*
        GameModeManager.StartLevel -= ActivateLevel;
        GameModeManager.PauseOn -= PauseMenu;
        GameModeManager.Success -= SuccessMenuOnLevel;
        GameModeManager.Fail -= FailedMenuOnLevel;
        GameModeManager.NestCount -= StartLevelEndCount;
        GameModeManager.NestCountEnd -= CancelLevelEndCount;
        */
    }
}

