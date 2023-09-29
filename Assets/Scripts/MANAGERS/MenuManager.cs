using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
using System;

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
    public GameObject pauseButton;
    public GameObject pauseMenu;
    public GameObject successMenu;
    public GameObject gameOverMenu;

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



    //----------------------------------------------



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

    public void ToMainMenu(int sceneIndex)
    {
        GetStoredTimerData();
        
        GameModeManager.instance.ChangeLevel(sceneIndex);
    }

    //----------------------------------------------

    public void GetStoredTimerData()
    {
        GameModeManager.instance.timerLevel1 = PlayerPrefs.GetFloat("Timer1_1");
        GameModeManager.instance.timerLevel2 = PlayerPrefs.GetFloat("Timer1_2");
        GameModeManager.instance.timerLevel3 = PlayerPrefs.GetFloat("Timer1_3");
    }

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

