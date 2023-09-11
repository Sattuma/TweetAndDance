using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject timerText;
    public GameObject pointsText;
    public GameObject timerCountText;
    public GameObject pointsCountText;
    public GameObject[] countDown = new GameObject[3];
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject levelClear;
    public GameObject gameOverHud;

    public GameObject level1Info;
    public GameObject level2Info;
    public GameObject level3Info;

    //public float timeValue = 10f;
    public TextMeshProUGUI timerCountdown;

    public bool isCountingLevel1;

    private void Awake()
    {

    }

    private void Start()
    {
        AudioManager.instance.PlayMusicFX(1);
    }
    void Update()
    {

        if (GameModeManager.instance.timerLevel1 > 0 && GameModeManager.instance.levelActive == true && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
        { GameModeManager.instance.timerLevel1 -= Time.deltaTime; }


        if (GameModeManager.instance.timerLevel1 <= 0 && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
        {
            GameModeManager.instance.levelActive = false;
            GameModeManager.instance.InvokeLevelFail();
            GameModeManager.instance.timerLevel1 = 0.1f;
        }

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
        {
            DisplayTime(GameModeManager.instance.timerLevel1);
        }
    }

    public void StartCountForLevelInfoOff()
    {
        StartCoroutine(InfoOffCount());
    }
    public IEnumerator InfoOffCount()
    {
        yield return new WaitForSecondsRealtime(1f);
        LevelInfoOff();
        pauseButton.SetActive(true);

    }
    //ALL LEVELS - LEVELINFO IN THE BEGINNING
    public void LevelInfoOff()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
        {
            level1Info.SetActive(false);
            timerText.SetActive(true);
            timerCountText.SetActive(true);
        }
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel)
        {
            level2Info.SetActive(false);
        }

    }

    //LEVEL1 - DISPLAY TIMECOUNT TO HUD
    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerCountdown.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //LEVEL1 - COUNT TO SUCCESS & FAILED HUD
    public void StartEndGame()
    {
        StartCoroutine(LevelEndCountOne());
    }
    public IEnumerator LevelEndCountOne()
    {
        isCountingLevel1 = true;
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
        SuccessMenuOnLevel1();
        isCountingLevel1 = false;
    }
    public void CancelEndGameHud()
    {
        StopAllCoroutines();

        isCountingLevel1 = false;

        countDown[0].SetActive(false);
        countDown[1].SetActive(false);
        countDown[2].SetActive(false);
    }
    public void SuccessMenuOnLevel1()
    {
        levelClear.SetActive(true);
        pauseButton.SetActive(false);
        GameModeManager.instance.levelActive = false;
    }
    public void FailedHud()
    {
        gameOverHud.SetActive(true);
    }

    //LEVEL2 - EVENT CALLS
    public void UpdateScore()
    {
        //pointsCountText.GetComponent<TextMeshProUGUI>().text = GameModeManager.instance.scoreLevel2.ToString() + "/ 2000";
    }
    public void LevelTwoCleared()
    {

    }

    // BUTTON CALLS
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

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


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

}

