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

    public EventSystem eventSystem;
    public GameObject lastSelectedGameObject;
    public GameObject currentSelectedGameObject_Recent;

    [Header("UI Timer HUDvariables")]
    public GameObject timerText;
    public GameObject timerCountText;

    [Header("UI points HUDvariables")]
    public GameObject pointsText;
    public GameObject pointsCountText;

    [Header("ACTIVE LEVEL HUDvariables")]
    public GameObject[] currentStateHud;
    public GameObject[] cutSceneInfo;

    [Header("UI Texts")]
    public GameObject[] beginning = new GameObject[2];
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
    public GameObject pauseFirstButton;
    public GameObject successFirstButton;
    public GameObject gameoverFirstButton;
    public GameObject settingsFirstButton;

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

    [Header("UI Sliders")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;

    [Header("Bonus Level Name After Level")]
    public string nextLevelName;
    public string bonusLevelName;
    public string mainMenuName;

    bool gamepadDetection;

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

        currentControlText.text = DataManager.instance.controls.ToString();

        beginning[0].SetActive(false);
        beginning[1].SetActive(false);
        pauseButton.SetActive(false);

        CheckCutSceneInfo(); // tsekataan mikä cutscene info tulee riippuen mikä kenttä on ja mitä tarvii

    }

    private void CheckCutSceneInfo()
    {
        //TÄSSÄ AKTIVOIDAAN CUTSCENE HUD PÄÄLLE ->
        CutsceneHudActive();

        int levelIndex = GameModeManager.instance.levelIndex;
        bool bonus = GameModeManager.instance.bonusLevelActive;

        //TÄSSÄ TSEKATAAN AKTIIVISEN LEVELIN CUTSCENE INFO LAITETAAN PÄÄLLE ->
        // tällä tavoin ekassa kolmeessa game levelissä on sama cutscene info. voi muuttaa vaihtamalla indexejä ja tekemällä lisää ui objecteja
        // voi spesifoida myöhemmin esim 1, 2 ,3 kenttiin eli saman maailman eri kenttiin eri infot jos tarvis !!!HUOM!!!
        if (levelIndex <= 3 && levelIndex > 0)             
        { 
            if(!bonus)
            { 
                cutSceneInfo[1].SetActive(true); // kenttien 1-3 cutscene info päälle kun index on 1 - 3
            }
            else if (bonus)
            { 
                cutSceneInfo[4].SetActive(true); // bonus 1 kentän cutscene info päälle kun index on 1 - 3
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

    private void ActivateLevel()
    { StartCoroutine(ActivateLevelCoRoutine()); }

    IEnumerator ActivateLevelCoRoutine()
    {
        yield return new WaitForSecondsRealtime(1f);
        beginning[0].SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        beginning[0].SetActive(false);
        beginning[1].SetActive(true);
        pauseButton.SetActive(true);
        GameModeManager.instance.StartCountOverInvoke();
        yield return new WaitForSecondsRealtime(0.5f);
        beginning[1].SetActive(false);
        CheckCurrentLevelInfo();
    }
    private void CheckCurrentLevelInfo()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
        { LevelHudActive(); }
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel)
        { BonusOneHudActive(); }

    }

    public void LevelHudActive()
    {
        currentStateHud[0].SetActive(false);
        currentStateHud[1].SetActive(true);
        currentStateHud[2].SetActive(false);
        currentStateHud[3].SetActive(true);
    }

    public void BonusOneHudActive()
    {
        currentStateHud[0].SetActive(false);
        currentStateHud[1].SetActive(false);
        currentStateHud[2].SetActive(true);
        currentStateHud[3].SetActive(true);
    }
    public void BonusTwoHudActive()
    {

    }
    public void BonusThreeHudActive()
    {

    }

    public void OnPointerEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            currentSelectedGameObject_Recent = eventSystem.currentSelectedGameObject;
            EventSystem.current.SetSelectedGameObject(null);
            lastSelectedGameObject = currentSelectedGameObject_Recent;
        }
    }
    public void OnPointerExit()
    {
        EventSystem.current.SetSelectedGameObject(currentSelectedGameObject_Recent);

        if (lastSelectedGameObject == currentSelectedGameObject_Recent)
        {
            lastSelectedGameObject = currentSelectedGameObject_Recent;
            currentSelectedGameObject_Recent = eventSystem.currentSelectedGameObject;
        }
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
        successFirstButton.GetComponent<Selectable>().Select();
    }
    public void FailedMenuOnLevel()
    {
        GameModeManager.instance.levelActive = false;
        gameOverMenu.SetActive(true);
        gameoverFirstButton.GetComponent<Selectable>().Select();
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
                pauseMenu.SetActive(true);
                pauseFirstButton.GetComponent<Selectable>().Select();
                Time.timeScale = 0;
            }
            if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel)
            {
                pauseMenuBonus.SetActive(true);
                pauseFirstButton.GetComponent<Selectable>().Select();
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
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
        { pauseMenu.SetActive(true);}
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel)
        { pauseMenuBonus.SetActive(true);}
    }
    void PauseMenuOff()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
        { pauseMenu.SetActive(false); }
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel)
        { pauseMenuBonus.SetActive(false); }
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
        GameObject.Find("AudioSettingsButton").GetComponent<Selectable>().Select();
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
        GameObject.Find("Master").GetComponent<Selectable>().Select();
    }
    public void OpenControlsSettings()
    {
        AudioManager.instance.PlayMenuFX(0);
        GameModeManager.instance.cannotResumeFromPause = true;
        backButton.SetActive(false);
        backButtonSpesific.SetActive(true);
        settingsMenu.SetActive(false);
        controlSettingsWindow.SetActive(true);
        GameObject.Find("SwitchControlsButton").GetComponent<Selectable>().Select();
    }
    public void OpenBackSpesific()
    {
        AudioManager.instance.PlayMenuFX(0);
        settingsMenu.SetActive(true);
        controlSettingsWindow.SetActive(false);
        audioSettingsWindow.SetActive(false);
        backButton.SetActive(true);
        backButtonSpesific.SetActive(false);
        GameObject.Find("AudioSettingsButton").GetComponent<Selectable>().Select();
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

        if (!gamepadDetection && Gamepad.current?.IsActuated(0) == null)
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
        pauseFirstButton.GetComponent<Selectable>().Select();
    }
    //------------------------------------------------

    //LEVEL2 - EVENT CALLS
    public void UpdateScore()
    {
        //pointsCountText.GetComponent<TextMeshProUGUI>().text = GameModeManager.instance.scoreLevel2.ToString() + "/ 2000";
    }

    // BUTTON CALLS FOR NAVIGATION BETWEEN MENUS AND SCENES
    public void Continue()
    {
        AudioManager.instance.PlayMenuFX(0);
        SetData();
        GameModeManager.instance.CheckBonusLevelAccess();
    }

    public void Retry()
    {
        AudioManager.instance.PlayMenuFX(0);
        SetData();
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
        AudioManager.instance.PlayMenuFX(0);
        SetData();
        GameModeManager.instance.levelActive = false;
        GameModeManager.instance.ChangeLevel(GameModeManager.instance.levelName[0]);
    }

    public void GetData()
    {
        masterVolumeSlider.value = AudioManager.instance.masterVolumeValue;
        effectsVolumeSlider.value = AudioManager.instance.effectsVolumeValue;
        musicVolumeSlider.value = AudioManager.instance.musicVolumeValue;

        Debug.Log("HUOM TÄÄLLÄ - testiä varten");
        //kommentoitu pois testiä varten, muuten päällä
        //DataManager.instance.GetLevelAudio();
    }
    public void SetData()
    {
        AudioManager.instance.masterVolumeValue = masterVolumeSlider.value;
        AudioManager.instance.effectsVolumeValue = effectsVolumeSlider.value;
        AudioManager.instance.musicVolumeValue = musicVolumeSlider.value;
        DataManager.instance.SetLevelAudio(masterVolumeSlider.value, effectsVolumeSlider.value, musicVolumeSlider.value);
    }

}

