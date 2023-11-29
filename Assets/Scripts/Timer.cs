using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerCountdown;
    public Animator uiAnim;

    void Update()
    {
        if (GameModeManager.instance.difficulty == GameModeManager.Difficulty.Normal)
        { 
            DisplayTime(GameModeManager.instance.timerNormalMode);

            if (GameModeManager.instance.timerNormalMode > 0 && GameModeManager.instance.levelActive == true && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
            { GameModeManager.instance.timerNormalMode-= Time.deltaTime; }

            if (GameModeManager.instance.timerNormalMode <= 0 && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
            {
                GameModeManager.instance.levelActive = false;
                GameModeManager.instance.InvokeLevelFail();
                GameModeManager.instance.timerNormalMode = 0.1f;
            }
        }

        if (GameModeManager.instance.difficulty == GameModeManager.Difficulty.Hard)
        {
            DisplayTime(GameModeManager.instance.timerHardMode);
            if (GameModeManager.instance.timerHardMode > 0 && GameModeManager.instance.levelActive == true && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
            { GameModeManager.instance.timerHardMode -= Time.deltaTime; }

            if (GameModeManager.instance.timerHardMode <= 0 && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
            {
                GameModeManager.instance.levelActive = false;
                GameModeManager.instance.InvokeLevelFail();
                GameModeManager.instance.timerHardMode = 0.1f;
            }

        }

    }


    public void DisplayTime(float timeToDisplay)
    {

        if(timeToDisplay <= 11 && timeToDisplay > 0.1f)
        { uiAnim.SetBool("CountOn", true);}
        else if(timeToDisplay < 0.1f)
        { uiAnim.SetBool("CountOn", false);}

        if (timeToDisplay > 11)
        { uiAnim.SetBool("CountOn", false); }

        if (timeToDisplay <= 0)
        { timeToDisplay = 0;}

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerCountdown.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }



}
