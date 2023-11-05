using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelScript : MonoBehaviour
{
    private void Awake()
    {

        GameModeManager.instance.bonusLevelActive = true;

        GameModeManager.StartLevelCountOver += ActivateLevel;

        if (GameModeManager.instance.levelIndex > 0 && GameModeManager.instance.levelIndex <= 3)
        {
            string level = GameModeManager.instance.bonusLevelName[1];
            GameModeManager.instance.ActivateCurrentLevel(level);
            //musa ja index 
        }
        if (GameModeManager.instance.levelIndex > 3 && GameModeManager.instance.levelIndex <= 6)
        {
            string level = GameModeManager.instance.bonusLevelName[2];
            GameModeManager.instance.ActivateCurrentLevel(level);
            //musa ja index 
        }
        if (GameModeManager.instance.levelIndex > 6 && GameModeManager.instance.levelIndex <= 9)
        {
            string level = GameModeManager.instance.bonusLevelName[3];
            GameModeManager.instance.ActivateCurrentLevel(level);
            //musa ja index 
        }
        GameModeManager.instance.CutSceneActive();
    }

    private void Start()
    {
        StartCoroutine(StartLevelCheck());
    }

    IEnumerator StartLevelCheck()
    {
        yield return new WaitUntil(() => GameModeManager.instance.cutsceneActive == false);
        GameModeManager.instance.StartLevelInvoke();
    }
    private void ActivateLevel()
    { GameModeManager.instance.BonusLevelActive(); }
}
