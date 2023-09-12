using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject timerText;
    public GameObject timerCountText;

    public GameObject pointsText;
    public GameObject pointsCountText;

    public GameObject[] countDown = new GameObject[3];

    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject levelClear;
    public GameObject gameOverHud;

    public bool isCountingLevel1;

    private void Start()
    {
        //AudioManager.instance.PlayMusicFX(1);
    }

    //kun gamemangerissa invokataan? nii aktivoidaan tämä?
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
        SuccessMenuOnLevel();
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

    public void SuccessMenuOnLevel()
    {
        GameModeManager.instance.levelActive = false;
    }

    public void FailedHud()
    {
        
    }

    //LEVEL2 - EVENT CALLS
    public void UpdateScore()
    {
        //pointsCountText.GetComponent<TextMeshProUGUI>().text = GameModeManager.instance.scoreLevel2.ToString() + "/ 2000";
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

