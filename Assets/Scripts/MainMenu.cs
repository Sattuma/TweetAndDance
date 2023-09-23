using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    [Header("MainMenu Buttons")]
    public GameObject startButton;
    public GameObject settingButton;
    public GameObject howToPlayButton;
    public GameObject creditsButton;
    public GameObject backButton;
    public GameObject quitButton;
    public GameObject mainButtons;

    [Header("Settings Window")]
    public GameObject settingWindow;

    [Header("HowToPlay Window")]
    public GameObject howToPlayWindow;
    public GameObject[] howToPlayImagesKey;
    public GameObject[] howToPlayImagesController;
    public GameObject howToPlayNavigationButtonLeft, howToPlayNavigationButtonRight;

    [Header("Credits Window")]
    public GameObject creditsWindow;


    public GameObject logo;

    private void Start()
    {
        AudioManager.instance.PlayMusicFX(0);
    }

    public void OnPointerEnter()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void ContinueGame()
    {
        Debug.Log("Continue Game Clicked");
    }

    public void StartGame(string levelIndex)
    {
        StartCoroutine(StartButtonDelay(levelIndex));
    }

    public void OpenSettings()
    {
        Debug.Log("Settings opened");
        AudioManager.instance.PlayMenuFX(0);
        mainButtons.SetActive(false);
        backButton.SetActive(true);
        settingWindow.SetActive(true);
        logo.SetActive(false);
        GameObject.Find("Master").GetComponent<Selectable>().Select();
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

    public void QuitGame()
    {
        Debug.Log("Quit Button Clicked");
        StartCoroutine(QuitButtonDelay());
    }

    public IEnumerator StartButtonDelay(string levelIndex)
    {
        AudioManager.instance.PlayMenuFX(0);
        yield return new WaitForSecondsRealtime(0.2f);
        AudioManager.instance.musicSource.Stop();
        SceneManager.LoadScene(levelIndex);
    }

    public IEnumerator QuitButtonDelay()
    {
        AudioManager.instance.PlayMenuFX(0);
        yield return new WaitForSecondsRealtime(0.2f);
        Application.Quit();
    }


}
