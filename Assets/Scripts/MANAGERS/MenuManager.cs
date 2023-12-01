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

    [Header("ONSCREEN UI LEVEL")]
    public GameObject timerText;
    public GameObject timerCountText;
    public Image secretImageToShow;
    public Sprite[] secretSpritesVariation; // linked to levelindex
    public TextMeshProUGUI secretCurrentText;
    public TextMeshProUGUI secretTotalText;

    [Header("ONSCREEN UI BONUS")]
    public Slider bonusOneSlider;

    [Header("ACTIVE LEVEL HUDvariables")]
    public GameObject[] currentStateHud;
    public GameObject[] cutSceneInfo;

    [Header("UI Texts")]
    public GameObject rewardText;
    public GameObject getReadyText;
    public GameObject[] beginning = new GameObject[2];
    public TextMeshProUGUI levelInfoText;
    public TextMeshProUGUI currentControlText;
    public TextMeshProUGUI noGamepadDetectedText;
    public TextMeshProUGUI foundSecretsText;
    public TextMeshProUGUI totalSecretsText;


    [Header("UI OnScreen CountDown")]
    public GameObject[] countDown = new GameObject[3];

    [Header("Buttons")]
    public GameObject cutSceneDemoButton;
    public GameObject pauseButton;
    public GameObject backButton;
    public GameObject backButtonSpesific;


    [Header("Buttons to be autoactivated on menus")]
    public GameObject firstButtonActiveUi;

    [Header("UI Menus")]
    public GameObject pauseMenu;
    public GameObject pauseMenuBonus;
    public GameObject settingsMenu;
    public GameObject audioSettingsWindow;
    public GameObject controlSettingsWindow;
    public GameObject successMenu;
    public GameObject gameOverMenu;

    [Header("UI BONUS INFO CUTSCENE VARIABLES")]
    public GameObject[] bonusControlLevel1_key = new GameObject[4];
    public GameObject[] bonusControlLevel1_pad = new GameObject[4];

    [Header("UI BONUSLEVEL")]
    public GameObject bonusOneMeter;
    public TextMeshProUGUI bonus1SongtimeText;
    public TextMeshProUGUI bonus1SongtimeTotalText;
    public GameObject successMenuBonus;
    public GameObject gameOverMenuBonus;

    [Header("UI Sliders")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;

    [Header("UI Animators")]
    public Animator bonusAnim;

    [Header("Bonus Level Name After Level")]
    public string nextLevelName;
    public string bonusLevelName;
    public string mainMenuName;

    bool gamepadDetection;

    private void Start()
    {

        GameModeManager.ControllerCheck += ControllerCheck;
        GameModeManager.SecretCountForMenu += DisplayStartSecrets;
        GameModeManager.RewardLevel += ActivateReward;
        GameModeManager.StartLevel += ActivateLevel;

        GameModeManager.PauseOn += PauseMenu;
        GameModeManager.Success += SuccessMenuOnLevel;
        GameModeManager.Fail += FailedMenuOnLevel;

        GameModeManager.BonusSuccess += SuccessMenuOnBonus;
        GameModeManager.BonusFail += FailedMenuOnBonus;

        GameModeManager.BonusMeterAnimOn += BonusAnimOn;
        GameModeManager.BonusMeterAnimOff += BonusAnimOff;

        GameModeManager.NestCount += StartLevelEndCount;
        GameModeManager.NestCountEnd += CancelLevelEndCount;

        Cursor.visible = true;

        currentControlText.text = DataManager.instance.controls.ToString();

        beginning[0].SetActive(false);
        beginning[1].SetActive(false);
        pauseButton.SetActive(false);

        GetAudioDataForUI();
        CheckCutSceneInfo(); // tsekataan mik� cutscene info tulee riippuen mik� kentt� on ja mit� tarvii
    }

    private void DisplayStartSecrets()
    {
        secretImageToShow.GetComponent<Image>().sprite = secretSpritesVariation[GameModeManager.instance.levelIndex];
        secretCurrentText.text = GameModeManager.instance.secretCurrentForMenu.ToString();
        secretTotalText.text = "/ ".ToString() + GameModeManager.instance.secretTotalForMenu.ToString();
    }

    private void BonusAnimOn()
    { bonusAnim.SetBool("Danger", true);  }
    private void BonusAnimOff()
    { bonusAnim.SetBool("Danger", false); }

    private void FixedUpdate()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel && !GameModeManager.instance.bonusLevelEnd)
        { UpdateScoreBonus(); DisplayTime(GameModeManager.instance.bonusOneSongTimeTemp, bonus1SongtimeText); }
    }

    private void CheckCutSceneInfo()
    {
        //T�SS� AKTIVOIDAAN CUTSCENE HUD P��LLE ->
        CutsceneHudActive();

        int levelIndex = GameModeManager.instance.levelIndex;
        bool bonus = GameModeManager.instance.bonusLevelActive;

        //T�SS� TSEKATAAN AKTIIVISEN LEVELIN CUTSCENE INFO LAITETAAN P��LLE ->
        // t�ll� tavoin ekassa kolmeessa game leveliss� on sama cutscene info. voi muuttaa vaihtamalla indexej� ja tekem�ll� lis�� ui objecteja
        // voi spesifoida my�hemmin esim 1, 2 ,3 kenttiin eli saman maailman eri kenttiin eri infot jos tarvis !!!HUOM!!!
        if (levelIndex <= 3 && levelIndex > 0)             
        { 
            if(!bonus)
            { 
                cutSceneInfo[1].SetActive(true); // kenttien 1-3 cutscene info p��lle kun index on 1 - 3
                levelInfoText.text = "Level " + GameModeManager.instance.levelIndex.ToString();
            }
            else if (bonus)
            { 
                cutSceneInfo[4].SetActive(true); // bonus 1 kent�n cutscene info p��lle kun index on 1 - 3
                if(DataManager.instance.controls == DataManager.ControlSystem.Keyboard)
                {
                    for (int i = 0; i < bonusControlLevel1_key.Length; i++)
                    { bonusControlLevel1_key[i].SetActive(true);}
                }
                else if (DataManager.instance.controls == DataManager.ControlSystem.Gamepad)
                {
                    for (int i = 0; i < bonusControlLevel1_key.Length; i++)
                    { bonusControlLevel1_pad[i].SetActive(true); }
                }
            }
        }
    }

    public void CutsceneHudActive()
    {
        
        currentStateHud[0].SetActive(true);
        currentStateHud[1].SetActive(false);
        currentStateHud[2].SetActive(false);
        currentStateHud[3].SetActive(false);
    }
    void ActivateReward()
    {
        rewardText.SetActive(true);
    }
    private void ActivateLevel()
    {
        StartCoroutine(ActivateLevelCoRoutine());
    }

    IEnumerator ActivateLevelCoRoutine()
    {
        //rewardText.SetActive(true);
        //yield return new WaitUntil(() => GameModeManager.instance.rewardClaimed == false);
        rewardText.SetActive(false);
        getReadyText.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        beginning[0].SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        beginning[0].SetActive(false);
        beginning[1].SetActive(true);
        GameModeManager.instance.StartCountOverInvoke();
        yield return new WaitForSecondsRealtime(0.5f);
        beginning[1].SetActive(false);
        CheckCurrentLevelInfo();



    }
    private void CheckCurrentLevelInfo()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
        { LevelHudActive(); GameModeManager.instance.InvokeLevelActiveOn(); }
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel)
        { BonusOneHudActive(); }
    }

    public void LevelHudActive()
    {
        currentStateHud[0].SetActive(false);
        currentStateHud[1].SetActive(true);
        currentStateHud[2].SetActive(false);
        currentStateHud[3].SetActive(true);
        pauseButton.SetActive(true);
    }

    public void BonusOneHudActive()
    {
        currentStateHud[0].SetActive(false);
        currentStateHud[1].SetActive(false);
        currentStateHud[2].SetActive(true);
        currentStateHud[3].SetActive(true);
        pauseButton.SetActive(true);
        DisplayTime(GameModeManager.instance.bonusOneSongTimeTotalTemp, bonus1SongtimeTotalText);
    }
    public void BonusTwoHudActive()
    {

    }
    public void BonusThreeHudActive()
    {

    }

    /*
    public void OnPointerEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            currentSelectedGameObject_Recent = eventSystem.currentSelectedGameObject;
            EventSystem.current.SetSelectedGameObject(null);
            lastSelectedGameObject = currentSelectedGameObject_Recent;
        }
    }
    */
    public void OnPointerEnter()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            GameObject myEventSystem = GameObject.Find("EventSystem");
            myEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
        }
    }
    public void OnPointerExit()
    {
        FindFirstButton(firstButtonActiveUi);
    }



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

    //GAME LEVEL ENDING MENU WINDOWS
    public void SuccessMenuOnLevel()
    {
        GameModeManager.instance.levelActive = false;
        successMenu.SetActive(true);
        foundSecretsText.text = GameModeManager.instance.secretFoundTemp.ToString();
        totalSecretsText.text = GameModeManager.instance.secretTotalTemp.ToString();
        FindFirstButton(firstButtonActiveUi);
    }
    public void FailedMenuOnLevel()
    {
        GameModeManager.instance.levelActive = false;
        gameOverMenu.SetActive(true);
        FindFirstButton(firstButtonActiveUi);
    }

    //BONUS LEVEL ENDING WINDOWS
    public void SuccessMenuOnBonus()
    {
        bonusOneSlider.gameObject.SetActive(false);
        successMenuBonus.SetActive(true);
        FindFirstButton(firstButtonActiveUi);
    }
    public void FailedMenuOnBonus()
    {
        AudioManager.instance.PlayBonusOneFX(0);
        AudioManager.instance.musicSource.Stop();
        gameOverMenuBonus.SetActive(true);
        FindFirstButton(firstButtonActiveUi);
    }
    //------------------------------------------------
    //PAUSE MENU WINDOW FUNCTIONS
    public void PauseMenu()
    {
        AudioManager.instance.PlayMenuFX(0);
        if (Time.timeScale == 1)
        {
            GameModeManager.instance.isPaused = true;
            if(GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
            {
                //pauseMenu.SetActive(true);
                PauseMenuOn();
                
                Time.timeScale = 0;
            }
            if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel)
            {
                //pauseMenuBonus.SetActive(true);
                PauseMenuOn();
                
                Time.timeScale = 0;
            }
        }
        else
        {
            GameModeManager.instance.isPaused = false;
            PauseMenuOff();
            Time.timeScale = 1;
        }
    }

    void PauseMenuOn()
    {
        Cursor.visible = true;

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
        { pauseMenu.SetActive(true); FindFirstButton(firstButtonActiveUi); }
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel)
        { pauseMenuBonus.SetActive(true); FindFirstButton(firstButtonActiveUi); AudioManager.instance.musicSource.Pause(); }
    }
    void PauseMenuOff()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
        { pauseMenu.SetActive(false); }
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel)
        { pauseMenuBonus.SetActive(false); AudioManager.instance.musicSource.Play(); }
    }
    public void OpenSettings()
    {
        AudioManager.instance.PlayMenuFX(0);
        GameModeManager.instance.cannotResumeFromPause = true;
        pauseButton.SetActive(false);
        AudioManager.instance.PlayMenuFX(0);
        settingsMenu.SetActive(true);
        PauseMenuOff();
        backButton.SetActive(true);
        FindFirstButton(firstButtonActiveUi);
        backButton.SetActive(true);

    }
    public void OpenAudioSettings()
    {
        AudioManager.instance.PlayMenuFX(0);
        GameModeManager.instance.cannotResumeFromPause = true;
        backButton.SetActive(false);
        backButtonSpesific.SetActive(true);
        settingsMenu.SetActive(false);
        audioSettingsWindow.SetActive(true);
        FindFirstButton(firstButtonActiveUi);
    }
    public void OpenControlsSettings()
    {
        AudioManager.instance.PlayMenuFX(0);
        GameModeManager.instance.cannotResumeFromPause = true;
        backButton.SetActive(false);
        backButtonSpesific.SetActive(true);
        settingsMenu.SetActive(false);
        controlSettingsWindow.SetActive(true);
        FindFirstButton(firstButtonActiveUi);
    }
    public void OpenBackSpesific()
    {
        AudioManager.instance.PlayMenuFX(0);
        settingsMenu.SetActive(true);
        controlSettingsWindow.SetActive(false);
        audioSettingsWindow.SetActive(false);
        backButton.SetActive(true);
        backButtonSpesific.SetActive(false);
        SetAndStoreAudioData();
        FindFirstButton(firstButtonActiveUi);
    }
    public void SwitchControls()
    {
        AudioManager.instance.PlayMenuFX(0);
        if (DataManager.instance.controls == DataManager.ControlSystem.Keyboard)
        {
            DataManager.instance.ActivateGamePad();

        }
        else if (DataManager.instance.controls == DataManager.ControlSystem.Gamepad)
        {
            DataManager.instance.ActivateKeyboard();
        }

        currentControlText.text = DataManager.instance.controls.ToString();

        if (!gamepadDetection && !DataManager.instance.controllerConnected)
        {
            StartCoroutine(NoConnect());
        }
    }

    IEnumerator NoConnect()
    {
        gamepadDetection = true;
        noGamepadDetectedText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        noGamepadDetectedText.gameObject.SetActive(false);
        gamepadDetection = false;
    }

    public void BackButtonPause()
    {
        AudioManager.instance.PlayMenuFX(0);
        AudioManager.instance.PlayMenuFX(0);
        GameModeManager.instance.cannotResumeFromPause = false;
        pauseButton.SetActive(true);
        settingsMenu.SetActive(false);
        PauseMenuOn();
        backButton.SetActive(false);
        FindFirstButton(firstButtonActiveUi);
    }
    //------------------------------------------------

    //LEVEL2 - EVENT CALLS
    public void UpdateScore()
    {
        //pointsCountText.GetComponent<TextMeshProUGUI>().text = GameModeManager.instance.scoreLevel2.ToString() + "/ 2000";
    }
    public void UpdateScoreBonus()
    {
        if (GameModeManager.instance.levelIndex > 0 && GameModeManager.instance.levelIndex <= 3)
        {
            bonusOneSlider.value = GameModeManager.instance.bonusOnelevelScoreTemp * 0.0001f;

        }
        if (GameModeManager.instance.levelIndex > 3 && GameModeManager.instance.levelIndex <= 6)
        {
            
        }
        if (GameModeManager.instance.levelIndex > 6 && GameModeManager.instance.levelIndex <= 9)
        {

        }
    }

    // BUTTON CALLS FOR NAVIGATION BETWEEN MENUS AND SCENES
    public void Continue()
    {
        SetAndStoreAudioData();
        AudioManager.instance.PlayMenuFX(0);


        //--------------------------------------------------------------------------------
        Debug.Log("T��LL� EKAA BUILDIA VARTEN TOISEEN KENTT��N LOPPUVA TEMP KOODI");
        // if else on k�yt�ss� vain ekassa buildissa ku npeli loppuu kakkoskent�n j�lkeen

        if(GameModeManager.instance.currentLevel == GameModeManager.CurrentLevel.Level1_2)
        {
            GameModeManager.instance.ChangeLevel(GameModeManager.instance.endScreenName[0]);
        }
        else
        {
            GameModeManager.instance.rewardClaimed = false;
            GameModeManager.instance.CheckBonusLevelAccess();
        }
        //--------------------------------------------------------------------------------

        //t�m� on oikea ja k�yt�ss� jatkossa kun eka buildi on buildattu
        //GameModeManager.instance.CheckBonusLevelAccess();

    }
    public void ContinueFromBonus()
    {
        SetAndStoreAudioData();
        AudioManager.instance.PlayMenuFX(0);
        GameModeManager.instance.ActivateNextLevel();
    }

    public void Retry()
    {
        SetAndStoreAudioData();
        AudioManager.instance.PlayMenuFX(0);
        string currentName = SceneManager.GetActiveScene().name;
        GameModeManager.instance.ChangeLevel(currentName);
    }

    public void CutSceneDemo()
    {
        AudioManager.instance.PlayMenuFX(0);
        cutSceneDemoButton.SetActive(false);
        currentStateHud[0].SetActive(false);
        GameModeManager.instance.cutsceneActive = false;

    }

    public void ToMainMenu()
    {
        SetAndStoreAudioData();
        AudioManager.instance.PlayMenuFX(0);
        GameModeManager.instance.levelActive = false;
        GameModeManager.instance.rewardClaimed = false;
        GameModeManager.instance.DefaultValues();
        GameModeManager.instance.ChangeLevel(GameModeManager.instance.levelName[0]);

    }

    public void SetAndStoreAudioData()
    {

        AudioManager.instance.masterVolumeValue = masterVolumeSlider.value;
        AudioManager.instance.effectsVolumeValue = effectsVolumeSlider.value;
        AudioManager.instance.musicVolumeValue = musicVolumeSlider.value;

        DataManager.instance.SetLevelAudio(AudioManager.instance.masterVolumeValue, AudioManager.instance.effectsVolumeValue, AudioManager.instance.musicVolumeValue);
    }
    public void GetAudioDataForUI()
    {

        DataManager.instance.GetLevelAudio(AudioManager.instance.masterVolumeValue, AudioManager.instance.effectsVolumeValue, AudioManager.instance.musicVolumeValue);

        masterVolumeSlider.value = AudioManager.instance.masterVolumeValue;
        effectsVolumeSlider.value = AudioManager.instance.effectsVolumeValue;
        musicVolumeSlider.value = AudioManager.instance.musicVolumeValue;
    }

    private void ControllerCheck()
    {
        currentControlText.text = DataManager.instance.controls.ToString();
    }

    public void DisplayTime(float timeToDisplay, TextMeshProUGUI text)
    {
        if (timeToDisplay >= GameModeManager.instance.bonusOneSongTimeTotalTemp)
        { timeToDisplay = GameModeManager.instance.bonusOneSongTimeTotalTemp; }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void FindFirstButton(GameObject obj)
    {
        obj = GameObject.FindGameObjectWithTag("FirstButton");
        if (obj != null)
        {
            obj.GetComponent<Selectable>().Select();
        }
        else
        {
            obj = null;
        }

    }

    private void OnDestroy()
    {
        //GameModeManager.instance.ResetEvents();
    }

}

