using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using System;

public class MainMenu : MonoBehaviour
{

    [Header("Buttons to be autoactivated on menus")]
    public GameObject firstButtonActiveUi;
    public GameObject mouseSelButton;

    [Header("MainMenu Buttons")]
    public GameObject startButton;
    public GameObject settingButton;
    public GameObject howToPlayButton;
    public GameObject creditsButton;
    public GameObject backButton;
    public GameObject backButtonSpesific;
    public GameObject quitButton;
    public GameObject mainButtons;

    [Header("Settings Window")]
    public GameObject settingWindow;
    public GameObject controlSettingsWindow;
    public GameObject audioSettingsWindow;
    public GameObject difficultySettingsWindow;
    public TextMeshProUGUI currentDifficultyText;
    public TextMeshProUGUI currentControlText;
    public TextMeshProUGUI noGamepadDetectedText;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;

    [Header("HowToPlay Window")]
    public GameObject howToPlayWindow;
    public GameObject[] howToPlayImagesKey;
    public GameObject[] howToPlayImagesController;
    public GameObject howToPlayNavigationButtonLeft, howToPlayNavigationButtonRight;

    [Header("Credits Window")]
    public GameObject creditsWindow;

    public GameObject logo;
    public Vector2 mousePos;
    bool gamepadDetection;

    private void Start()
    {
        GameModeManager.ControllerCheck += ControllerCheck;

        AudioManager.instance.musicSource.loop = true;


        //DataManager.instance.CheckControllerNull(); // check ei toimi, pelin alussa herjaus jos gamepad ei ole kytketty. tee checki siit� ett� toimii my�s kun vain toinen KEY/ GAMEPAD on kytketty

        Cursor.visible = true;

        AudioManager.instance.PlayMusicFX(0);
        currentControlText.text = DataManager.instance.controls.ToString();
        currentDifficultyText.text = GameModeManager.instance.difficulty.ToString();

        GetAudioDataForUI();
    }



    /*
    public void OnPointerEnter()
    {
        //WHEN MOUSE IS SET AWAY FROM UI BUTTON
        if(EventSystem.current.IsPointerOverGameObject())
        {
            mousePos = Input.mousePosition;
            RectTransformUtility.RectangleContainsScreenPoint(rectTransform, mousePos);
            EventSystem.current.SetSelectedGameObject(null);
            rectTransform.GetComponent<Selectable>().Select();
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
    // EI TOIMI T�YSIN MUT OOKOO T�H�N TILANTEESEEN - FIX MY�HEMMIN

    public void OnPointerExit()
    {
        FindFirstButton(firstButtonActiveUi);
    }

    public void ContinueGame()
    {
        Debug.Log("Continue Game Clicked");
    }

    public void StartGame(string levelName)
    {
        levelName = GameModeManager.instance.levelName[1];
        StartCoroutine(StartButtonDelay(levelName));
    }

    public void OpenSettings()
    {
        AudioManager.instance.PlayMenuFX(0);
        mainButtons.SetActive(false);
        backButton.SetActive(true);
        settingWindow.SetActive(true);
        logo.SetActive(false);
        FindFirstButton(firstButtonActiveUi);
    }
    public void OpenAudioSettings()
    {
        AudioManager.instance.PlayMenuFX(0);
        backButton.SetActive(false);
        backButtonSpesific.SetActive(true);
        settingWindow.SetActive(false);
        audioSettingsWindow.SetActive(true);
        FindFirstButton(firstButtonActiveUi);
    }
    public void OpenControlsSettings()
    {
        AudioManager.instance.PlayMenuFX(0);
        backButton.SetActive(false);
        backButtonSpesific.SetActive(true);
        settingWindow.SetActive(false);
        controlSettingsWindow.SetActive(true);
        FindFirstButton(firstButtonActiveUi);
    }
    public void OpenDifficultySettings()
    {
        AudioManager.instance.PlayMenuFX(0);
        backButton.SetActive(false);
        backButtonSpesific.SetActive(true);
        settingWindow.SetActive(false);
        difficultySettingsWindow.SetActive(true);
        FindFirstButton(firstButtonActiveUi);
    }
    public void OpenBackSpesific()
    {
        AudioManager.instance.PlayMenuFX(0);
        settingWindow.SetActive(true);
        controlSettingsWindow.SetActive(false);
        audioSettingsWindow.SetActive(false);
        difficultySettingsWindow.SetActive(false);
        backButton.SetActive(true);
        backButtonSpesific.SetActive(false);
        SetAndStoreAudioData();
        FindFirstButton(firstButtonActiveUi);
    }
    public void SwitchControls()
    {
        AudioManager.instance.PlayMenuFX(0);
        if (DataManager.instance.controls == DataManager.ControlSystem.Keyboard)
        { DataManager.instance.ActivateGamePad(); }
        else if (DataManager.instance.controls == DataManager.ControlSystem.Gamepad)
        {DataManager.instance.ActivateKeyboard();}
        currentControlText.text = DataManager.instance.controls.ToString();

        if (!gamepadDetection && Gamepad.current?.IsActuated(0) == null)
        { StartCoroutine(NoConnect());}
    }
    public void SwitchDifficulty()
    {
        AudioManager.instance.PlayMenuFX(0);
        if (GameModeManager.instance.difficulty == GameModeManager.Difficulty.Normal)
        { GameModeManager.instance.difficulty = GameModeManager.Difficulty.Hard;}
        else if (GameModeManager.instance.difficulty == GameModeManager.Difficulty.Hard)
        { GameModeManager.instance.difficulty = GameModeManager.Difficulty.Normal;}
        currentDifficultyText.text = GameModeManager.instance.difficulty.ToString();
    }

    IEnumerator NoConnect()
    {
        gamepadDetection = true;
        noGamepadDetectedText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        noGamepadDetectedText.gameObject.SetActive(false);
        gamepadDetection = false;
    }
    private void ControllerCheck()
    {
        currentControlText.text = DataManager.instance.controls.ToString();
    }
    public void OpenHowToPlay()
    {
        Debug.Log("HowToPlay Opened");
        AudioManager.instance.PlayMenuFX(0);
        mainButtons.SetActive(false);
        backButton.SetActive(true);
        howToPlayWindow.SetActive(true);
        logo.SetActive(false);
        howToPlayNavigationButtonRight.SetActive(true);
        howToPlayNavigationButtonLeft.SetActive(false);
        //howToPlayNavigationButtonRight.GetComponent<Selectable>().Select();
        FindFirstButton(firstButtonActiveUi);

        for (int i = 0; i < howToPlayImagesController.Length; i++)
        {
            howToPlayImagesController[i].SetActive(false);
        }
        for (int i = 0; i < howToPlayImagesKey.Length; i++)
        {
            howToPlayImagesKey[i].SetActive(true);
        }
    }

    public void HowToPlayRightNav()
    {
        AudioManager.instance.PlayMenuFX(0);
        howToPlayNavigationButtonRight.SetActive(false);
        howToPlayNavigationButtonLeft.SetActive(true);
        //howToPlayNavigationButtonLeft.GetComponent<Selectable>().Select();
        FindFirstButton(firstButtonActiveUi);

        for (int i = 0; i < howToPlayImagesController.Length; i++)
        {
            howToPlayImagesController[i].SetActive(true);
        }
        for (int i = 0; i < howToPlayImagesKey.Length; i++)
        {
            howToPlayImagesKey[i].SetActive(false);
        }
    }

    public void HowToPlayLeftNav()
    {
        AudioManager.instance.PlayMenuFX(0);
        howToPlayNavigationButtonRight.SetActive(true);
        howToPlayNavigationButtonLeft.SetActive(false);
        //howToPlayNavigationButtonRight.GetComponent<Selectable>().Select();
        FindFirstButton(firstButtonActiveUi);

        for (int i = 0; i < howToPlayImagesController.Length; i++)
        {
            howToPlayImagesController[i].SetActive(false);
        }
        for (int i = 0; i < howToPlayImagesKey.Length; i++)
        {
            howToPlayImagesKey[i].SetActive(true);
        }
    }
    public void OpenCredits()
    {
        Debug.Log("Credits Opened");
        AudioManager.instance.PlayMenuFX(0);
        mainButtons.SetActive(false);
        backButton.SetActive(true);
        creditsWindow.SetActive(true);
        logo.SetActive(false);
        FindFirstButton(firstButtonActiveUi);

    }
    public void OpenBack()
    {
        Debug.Log("Back Button Clicked");
        AudioManager.instance.PlayMenuFX(0);
        mainButtons.SetActive(true);
        backButton.SetActive(false);
        settingWindow.SetActive(false);
        howToPlayWindow.SetActive(false);
        creditsWindow.SetActive(false);
        logo.SetActive(true);
        FindFirstButton(firstButtonActiveUi);
    }

    public void ActivateKeyboard()
    {
        DataManager.instance.ActivateKeyboard();
    }
    public void ActivateGamepad()
    {
        DataManager.instance.ActivateGamePad();
    }

    public void QuitGame()
    {
        StartCoroutine(QuitButtonDelay());
    }

    public IEnumerator StartButtonDelay(string levelName)
    {
        AudioManager.instance.PlayMenuFX(0);
        SetAndStoreAudioData();
        yield return new WaitForSecondsRealtime(0.2f);
        GameModeManager.instance.ChangeLevel(levelName);
    }

    public IEnumerator QuitButtonDelay()
    {
        AudioManager.instance.PlayMenuFX(0);
        yield return new WaitForSecondsRealtime(0.2f);
        Application.Quit();
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

    }


}
