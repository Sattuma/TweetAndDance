using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HudScript : MonoBehaviour
{

    public GameObject timerText;
    public GameObject timerCountText;
    public GameObject[] countDown = new GameObject[3];
    public GameObject levelClearOne;
    public GameObject gameOverHud;
    public GameObject retryLevelButton;
    public NestScript nest;
    public PlayerCore core;

    public float timeValue = 10f;
    public TextMeshProUGUI timerCountdown;
    public HudScript hud;
    public GameObject aSync;

    void Update()
    {
        if (timeValue > 0 && GameModeManager.instance.level1Over != true) 
        { timeValue -= Time.deltaTime; }

        if (timeValue <= 0)
        {
            GameModeManager.instance.level1Over = true;
            FailedHud();
            levelClearOne.SetActive(false);
            CancelEndGameHud();
        }

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1)
        {
            DisplayTime(timeValue);
        }
    }

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

    public void StartEndGame()
    {
        StartCoroutine(LevelEndCountOne());
    }

    public IEnumerator LevelEndCountOne()
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
        GameModeManager.instance.level1Over = true;
        //GameModeManager.instance.LevelOneCleared();     thi active when level2 is on progress?
        LevelChangeMenu2On();
    }

    public void CancelEndGameHud()
    {
        StopAllCoroutines();
        countDown[0].SetActive(false);
        countDown[1].SetActive(false);
        countDown[2].SetActive(false);
    }

    public void LevelChangeMenu2On()
    {
        levelClearOne.SetActive(true);
    }

    public void LevelChangeMenu2off()
    {
        levelClearOne.SetActive(false);
        timerText.SetActive(false);
        timerCountText.SetActive(false);
        SceneManager.LoadScene("GameLevel");
        GameModeManager.instance.level1Over = false;
        //core.PlayerPosLevel2();
    }

    public void FailedHud()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1)
        {

            gameOverHud.SetActive(true);
        }
        else
        {
            gameOverHud.SetActive(false);
        }
    }

    public void Retry()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1)
        {
            SceneManager.LoadScene("GameLevel");
            levelClearOne.SetActive(false);
            gameOverHud.SetActive(false);
            GameModeManager.instance.level1Over = false;
        }

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level2)
        {
            //retry level2
            gameOverHud.SetActive(false);
        }
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level3)
        {
            //retry level3
            gameOverHud.SetActive(false);
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
