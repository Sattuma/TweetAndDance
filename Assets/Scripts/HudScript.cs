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
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject levelClear;
    public GameObject gameOverHud;
    public NestScript nest;
    public PlayerCore core;
    public Aino_Movement aino;

    public GameObject level1Info;
    public GameObject level2Info;
    public GameObject level3Info;

    //public float timeValue = 10f;
    public TextMeshProUGUI timerCountdown;
    public HudScript hud;
    public GameObject aSync;

    public bool isCountingLevel1;

    public NoteLineScript noteLine;

    private void Awake()
    {
        GameModeManager.Level2Score += UpdateScore;

        InputHandler.InfoBoxAnim += StartCountForLevelInfoOff;
        InputHandler.PauseOn += PauseMenu;

        GameModeManager.Level1End += LevelOneCleared;
        GameModeManager.Level2End += LevelTwoCleared;


    }

    private void Start()
    {
        level1Info.SetActive(true);
        timerText.SetActive(false);
        timerCountText.SetActive(false);
        nest.nestAnim.SetBool("Flash", true);
        nest.arrowAnim.SetBool("Flash", true);
        nest.arrow2Anim.SetBool("Flash", true);
        nest.arrowObj.SetActive(true);
        nest.arrow2Obj.SetActive(true);
        pauseButton.SetActive(false);
        AudioManager.instance.PlayMusicFX(1);
    }
    void Update()
    {

        if (GameModeManager.instance.timerLevel1 > 0 && GameModeManager.instance.levelActive == true && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1) 
        { GameModeManager.instance.timerLevel1 -= Time.deltaTime; }


        if (GameModeManager.instance.timerLevel1 <= 0 && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1)
        {
            GameModeManager.instance.levelActive = false;
            GameModeManager.instance.StopInvoke();
            FailedHud();
            levelClear.SetActive(false);
            CancelEndGameHud();
            GameModeManager.instance.InvokeLevelFail();
            GameModeManager.instance.timerLevel1 = 0.1f;
        }

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1)
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
        nest.nestAnim.SetBool("Flash", false);
        nest.arrowAnim.SetBool("Flash",false);
        nest.arrow2Anim.SetBool("Flash", false);
        nest.arrowObj.SetActive(false);
        nest.arrow2Obj.SetActive(false);
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
        if (GameModeManager.instance.scoreLevel2 >= 2000 && GameModeManager.instance.scoreEndCount >= GameModeManager.instance.scoreEndCountTarget)
        {
            GameModeManager.instance.LevelTwoCleared();
            levelClear.SetActive(true);
            aino.WinAnim();
        }
        else if(GameModeManager.instance.scoreEndCount >= GameModeManager.instance.scoreEndCountTarget)
        {
            gameOverHud.SetActive(true);
            GameModeManager.instance.InvokeLevelFail();
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
            AudioManager.instance.PlayMusicFX(3);
        }

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level2)
        {
            //kakkoskentän continue mene retryyn koska kolmokenttää ei ole vielä - muuta kun tulee
            Retry();
        }

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level3)
        {

        }
    }

    public void Retry()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1)
        {
            StopAllCoroutines();
            //SceneManager.LoadScene("GameLevel");
            levelClear.SetActive(false);
            gameOverHud.SetActive(false);
            pauseMenu.SetActive(false);
            level1Info.SetActive(true);
            timerText.SetActive(false);
            timerCountText.SetActive(false);
            GameModeManager.instance.DestroyPickUpsWithTag();
            GameModeManager.instance.DestroyPickUpsWithTag2();
            GameModeManager.instance.GroundSpawnPickupLevel1();
            GameModeManager.instance.timerLevel1 = 120;
            GameModeManager.instance.levelActive = false;
            GameModeManager.instance.CutScene1Active();
            core.myAnim.SetTrigger("ResetTrig");
            core.myAnim.SetFloat("x", 0);
            nest.nestAnim.SetBool("Flash", true);
            nest.arrowAnim.SetBool("Flash", true);
            nest.arrow2Anim.SetBool("Flash", true);
            nest.arrowObj.SetActive(true);
            nest.arrow2Obj.SetActive(true);
            pauseButton.SetActive(false);
            

            if (GameModeManager.instance.isPaused)
            {
                PauseMenu();
                
            }

        }

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level2)
        {
            StopAllCoroutines();
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
            core.myAnim.SetFloat("x", 0);
            pauseButton.SetActive(false);
            aino.anim.SetTrigger("Reset");
            aino.isFailed = false;
            core.myAnim.SetTrigger("Level1");
            AudioManager.instance.PlayMusicFX(3);

            if (GameModeManager.instance.isPaused)
            {
                PauseMenu();
                
            }
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

        // tähän game active pääll kun kaikki tarvittava on redi
        yield return new WaitForSeconds(2f);
        GameModeManager.instance.ainoOnMove = true;

        //yield return new WaitUntil(() => GameModeManager.instance.levelActive == true);
        yield return new WaitForSeconds(2f);
        GameModeManager.instance.levelActive = true;
        yield return new WaitForSeconds(2f);
        noteLineImage.SetActive(true);
        pointsText.SetActive(true);
        pointsCountText.SetActive(true);
        yield return new WaitForSeconds(2f);
        core.myAnim.SetTrigger("Level2");
        noteLine.InvokeStartLevel2();
        AudioManager.instance.PlayMusicFX(2);
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
