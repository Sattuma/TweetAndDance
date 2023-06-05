using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HudScript : MonoBehaviour
{

    public GameObject timerText;
    public GameObject timerCountText;
    public GameObject[] countDown = new GameObject[4];
    public GameObject levelClearOne;
    public GameObject gameOverHud;
    public GameObject retryLevelButton;
    public NestScript nest;
    public PlayerCore core;

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
        countDown[3].SetActive(true);
        GameModeManager.instance.LevelOneCleared();
        FailedHud();
        LevelChangeMenu2On();
    }

    public void CancelEndGameHud()
    {
        StopAllCoroutines();
        countDown[0].SetActive(false);
        countDown[1].SetActive(false);
        countDown[2].SetActive(false);
        countDown[3].SetActive(false);
    }

    public void LevelChangeMenu2On()
    {
        levelClearOne.SetActive(true);
    }

    public void LevelChangeMenu2off()
    {
        levelClearOne.SetActive(false);
        countDown[3].SetActive(false);
        timerText.SetActive(false);
        timerCountText.SetActive(false);
        core.PlayerPosLevel2();
    }

    public void FailedHud()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1)
        {
            gameOverHud.SetActive(true);
        }
        else
        {
            gameOverHud.SetActive(true);
        }
    }

    public void Retry()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1)
        {
            SceneManager.LoadScene("HenriScene");
            gameOverHud.SetActive(false);
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
