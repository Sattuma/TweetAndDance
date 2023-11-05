using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    public EventSystem eventSystem;
    public GameObject lastSelectedGameObject;
    public GameObject currentSelectedGameObject_Recent;

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

        AudioManager.instance.masterVolumeValue = masterVolumeSlider.value;
        AudioManager.instance.effectsVolumeValue = effectsVolumeSlider.value;
        AudioManager.instance.musicVolumeValue = musicVolumeSlider.value;

        //DataManager.instance.CheckControllerNull(); // check ei toimi, pelin alussa herjaus jos gamepad ei ole kytketty. tee checki siitä että toimii myös kun vain toinen KEY/ GAMEPAD on kytketty
        GetData();
        SetData();
        AudioManager.instance.PlayMusicFX(0);
        currentControlText.text = DataManager.instance.controls.ToString();


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

    // EI TOIMI TÄYSIN MUT OOKOO TÄHÄN TILANTEESEEN - FIX MYÖHEMMIN
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
        GameObject.Find("AudioSettingsButton").GetComponent<Selectable>().Select();
    }
    public void OpenAudioSettings()
    {
        AudioManager.instance.PlayMenuFX(0);
        backButton.SetActive(false);
        backButtonSpesific.SetActive(true);
        settingWindow.SetActive(false);
        audioSettingsWindow.SetActive(true);
        GameObject.Find("Master").GetComponent<Selectable>().Select();
    }
    public void OpenControlsSettings()
    {
        AudioManager.instance.PlayMenuFX(0);
        backButton.SetActive(false);
        backButtonSpesific.SetActive(true);
        settingWindow.SetActive(false);
        controlSettingsWindow.SetActive(true);
        GameObject.Find("SwitchControlsButton").GetComponent<Selectable>().Select();

    }
    public void OpenBackSpesific()
    {
        AudioManager.instance.PlayMenuFX(0);
        settingWindow.SetActive(true);
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
        howToPlayNavigationButtonRight.GetComponent<Selectable>().Select();

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
        howToPlayNavigationButtonLeft.GetComponent<Selectable>().Select();

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
        howToPlayNavigationButtonRight.GetComponent<Selectable>().Select();

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
        backButton.GetComponent<Selectable>().Select();

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
        startButton.GetComponent<Selectable>().Select();
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
        SetData();
        yield return new WaitForSecondsRealtime(0.2f);
        AudioManager.instance.musicSource.Stop();
        GameModeManager.instance.ChangeLevel(levelName);
    }

    public IEnumerator QuitButtonDelay()
    {
        AudioManager.instance.PlayMenuFX(0);
        yield return new WaitForSecondsRealtime(0.2f);
        Application.Quit();
    }

    public void SetData()
    {
        AudioManager.instance.masterVolumeValue = masterVolumeSlider.value;
        AudioManager.instance.effectsVolumeValue = effectsVolumeSlider.value;
        AudioManager.instance.musicVolumeValue = musicVolumeSlider.value;
        DataManager.instance.SetLevelAudio(masterVolumeSlider.value, effectsVolumeSlider.value, musicVolumeSlider.value);
    }

    public void GetData()
    {
        DataManager.instance.GetLevelAudio();
        masterVolumeSlider.value = AudioManager.instance.masterVolumeValue;
        effectsVolumeSlider.value = AudioManager.instance.effectsVolumeValue;
        musicVolumeSlider.value = AudioManager.instance.musicVolumeValue;
    }

    private void OnDestroy()
    {
        if(lastSelectedGameObject = null)
        { lastSelectedGameObject = null; }
        if(currentSelectedGameObject_Recent = null)
        { currentSelectedGameObject_Recent = null; }
    }


}
