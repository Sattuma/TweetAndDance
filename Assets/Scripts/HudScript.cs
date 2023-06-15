using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class HudScript : MonoBehaviour
{
    public GameObject noteLineImage;
    public GameObject timerText;
    public GameObject pointsText;
    public GameObject timerCountText;
    public GameObject pointsCountText;
    public GameObject[] countDown = new GameObject[3];
    public GameObject levelClear;
    public GameObject gameOverHud;
    public NestScript nest;
    public PlayerCore core;

    public GameObject level1Info;
    public GameObject level2Info;
    public GameObject level3Info;

    public float timeValue = 10f;
    public TextMeshProUGUI timerCountdown;
    public HudScript hud;
    public GameObject aSync;

    public NoteLineScript noteLine;

    private void Awake()
    {
        GameModeManager.Level2Score += UpdateScore;

        InputHandler.InfoBoxAnim += StartCountForLevelInfoOff;

        GameModeManager.Level1End += LevelOneCleared;
        GameModeManager.Level2End += LevelTwoCleared;


    }

    private void Start()
    {
        level1Info.SetActive(true);
        timerText.SetActive(false);
        timerCountText.SetActive(false);
    }
    void Update()
    {
        if (timeValue > 0 && GameModeManager.instance.levelActive == true && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1) 
        { timeValue -= Time.deltaTime; }


        if (timeValue <= 0 && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1)
        {
            GameModeManager.instance.levelActive = false;
            FailedHud();
            levelClear.SetActive(false);
            CancelEndGameHud();
        }

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1)
        {
            DisplayTime(timeValue);
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
    }
    //ALL LEVELS - LEVELINFO IN THE BEGINNING
    public void LevelInfoOff()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1)
        {
            level1Info.SetActive(false);
            timerText.SetActive(true);
            timerCountText.SetActive(true);
        }
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level2)
        {
            level2Info.SetActive(false);
        }
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level3)
        {
            //level 3 hudihommat
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
    }
    public void CancelEndGameHud()
    {
        StopAllCoroutines();
        countDown[0].SetActive(false);
        countDown[1].SetActive(false);
        countDown[2].SetActive(false);
    }
    public void SuccessMenuOnLevel1()
    {
        levelClear.SetActive(true);
        GameModeManager.instance.levelActive = false;
        GameModeManager.instance.LevelOneCleared();
    }
    public void FailedHud()
    {
        gameOverHud.SetActive(true);
    }

    //LEVEL2 - EVENT CALLS
    public void UpdateScore()
    {
        pointsCountText.GetComponent<TextMeshProUGUI>().text = GameModeManager.instance.scoreLevel2.ToString();
    }
    public void LevelTwoCleared()
    {
        if (GameModeManager.instance.scoreLevel2 >= 50 && GameModeManager.instance.scoreEndCount >= 5)
        {
            levelClear.SetActive(true);
        }
        else
        {
            gameOverHud.SetActive(true);
        }
    }

    // BUTTON CALLS
    public void Continue()
    {

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1)
        {
            levelClear.SetActive(false);
            level2Info.SetActive(true);
            timerText.SetActive(false);
            timerCountText.SetActive(false);
            GameModeManager.instance.CutScene2Active();
            core.myAnim.SetTrigger("ResetTrig");
        }

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level2)
        {
            levelClear.SetActive(false);
            level2Info.SetActive(true);
            timerText.SetActive(false);
            timerCountText.SetActive(false);
            GameModeManager.instance.CutScene3Active();
        }

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level3)
        {

        }
    }

    public void Retry()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1)
        {
            //SceneManager.LoadScene("GameLevel");
            levelClear.SetActive(false);
            gameOverHud.SetActive(false);
            level1Info.SetActive(true);
            GameModeManager.instance.levelActive = false;
            GameModeManager.instance.CutScene1Active();
            core.myAnim.SetTrigger("ResetTrig");
        }

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level2)
        {
            GameModeManager.instance.level2Retry = true;
            levelClear.SetActive(false);
            gameOverHud.SetActive(false);
            level2Info.SetActive(true);
            noteLineImage.SetActive(false);
            timerText.SetActive(false);
            timerCountText.SetActive(false);
            GameModeManager.instance.levelActive = false;
            GameModeManager.instance.LevelOneCleared();
            GameModeManager.instance.scoreLevel2 = 0;
            UpdateScore();
            GameModeManager.instance.CutScene2Active();
            core.myAnim.SetTrigger("ResetTrig");
        }
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level3)
        {
            //retry level3
            levelClear.SetActive(false);
            gameOverHud.SetActive(false);
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NoteImageActive()
    {
        noteLineImage.SetActive(true);
    }

    public void LevelOneCleared()
    {
        noteLineImage.SetActive(false);
        StartCoroutine(GameMode2Start());
    }

    public IEnumerator GameMode2Start()
    {

        yield return new WaitUntil(() => GameModeManager.instance.levelActive == true);
        //tapahtumat kaikissa muissa scripteissä redi nii sit vasta tästä eteenpäin
        yield return new WaitForSeconds(2f);
        noteLineImage.SetActive(true);
        pointsText.SetActive(true);
        pointsCountText.SetActive(true);
        yield return new WaitForSeconds(2f);
        noteLine.InvokeStartLevel2();
    }

}
