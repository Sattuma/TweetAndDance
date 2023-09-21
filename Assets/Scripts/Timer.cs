using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerCountdown;

    void Update()
    {
        if (GameModeManager.instance.currentLevel == GameModeManager.CurrentLevel.Level1_1)
        { 
            DisplayTime(GameModeManager.instance.timerLevel1);

            if (GameModeManager.instance.timerLevel1 > 0 && GameModeManager.instance.levelActive == true && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
            { GameModeManager.instance.timerLevel1 -= Time.deltaTime; }

            if (GameModeManager.instance.timerLevel1 <= 0 && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
            {
                GameModeManager.instance.levelActive = false;
                GameModeManager.instance.InvokeLevelFail();
                GameModeManager.instance.timerLevel1 = 0.1f;
            }
        }

        if (GameModeManager.instance.currentLevel == GameModeManager.CurrentLevel.Level1_2)
        {
            DisplayTime(GameModeManager.instance.timerLevel2);
            if (GameModeManager.instance.timerLevel2 > 0 && GameModeManager.instance.levelActive == true && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
            { GameModeManager.instance.timerLevel2 -= Time.deltaTime; }

            if (GameModeManager.instance.timerLevel2 <= 0 && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
            {
                GameModeManager.instance.levelActive = false;
                GameModeManager.instance.InvokeLevelFail();
                GameModeManager.instance.timerLevel2 = 0.1f;
            }

        }

        if (GameModeManager.instance.currentLevel == GameModeManager.CurrentLevel.Level1_3)
        {
            DisplayTime(GameModeManager.instance.timerLevel3);
            if (GameModeManager.instance.timerLevel3 > 0 && GameModeManager.instance.levelActive == true && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
            { GameModeManager.instance.timerLevel3-= Time.deltaTime; }

            if (GameModeManager.instance.timerLevel3 <= 0 && GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel)
            {
                GameModeManager.instance.levelActive = false;
                GameModeManager.instance.InvokeLevelFail();
                GameModeManager.instance.timerLevel3 = 0.1f;
            }
        }
    }


    public void DisplayTime(float timeToDisplay)
    {

        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerCountdown.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
