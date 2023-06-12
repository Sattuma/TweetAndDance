using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class HudScript : MonoBehaviour
{

    public GameObject timerText;
    public GameObject pointsText;
    public GameObject timerCountText;
    public GameObject pointsCountText;
    public GameObject[] countDown = new GameObject[3];
    public GameObject levelClearOne;
    public GameObject gameOverHud;
    public NestScript nest;
    public PlayerCore core;

    public GameObject level1Info;
    public GameObject levle2Info;

    public float timeValue = 10f;
    public TextMeshProUGUI timerCountdown;
    public HudScript hud;
    public GameObject aSync;

    public bool gameActive = false;

    public InputActionReference skipFunction;


    private void Start()
    {
        level1Info.SetActive(true);
        timerText.SetActive(false);
        timerCountText.SetActive(false);
    }
    void Update()
    {
        if (skipFunction.action.IsPressed() && !gameActive)
        {
            level1Info.SetActive(false);
            timerText.SetActive(true);
            timerCountText.SetActive(true);
            gameActive = true;
        }

        if (timeValue > 0 && GameModeManager.instance.level1Over != true && gameActive) 
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
        GameModeManager.instance.level1Over = true;
    }

    public void LevelChangeMenu2off()
    {
        levelClearOne.SetActive(false);
        timerText.SetActive(false);
        timerCountText.SetActive(false);
        pointsText.SetActive(true);
        pointsCountText.SetActive(true);
        GameModeManager.instance.LevelOneCleared();
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
