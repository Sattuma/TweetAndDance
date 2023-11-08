using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SCRIPT HANDLE BONUSLEVELLOGIC AND ACTIVATES THE CERTAIN BONSULEVELFUNCION/OBJECT/SCRIPT WHICH NEEDED BETWEEN LEVELS
public class BonusLevelScript : MonoBehaviour
{
    [Header("GameObjects ACtivation")]
    public GameObject bonus1obj;
    public GameObject bonus2obj;
    public GameObject bonus3obj;

    private void Awake()
    {
        GameModeManager.StartLevelCountOver += ActivateLevel;
    }

    private void Start()
    {
        AudioManager.instance.musicSource.loop = true;
        GameModeManager.instance.bonusLevelActive = true;
        DataManager.instance.CheckBonusLevelInfo(bonus1obj,bonus2obj,bonus3obj);
        GameModeManager.instance.CutSceneActive();

        StartCoroutine(StartLevelCheck());
    }

    IEnumerator StartLevelCheck()
    {
        yield return new WaitUntil(() => GameModeManager.instance.cutsceneActive == false);
        GameModeManager.instance.StartLevelInvoke();
    }
    private void ActivateLevel()
    { GameModeManager.instance.BonusLevelActive(); }

    private void OnDestroy()
    {
        GameModeManager.instance.bonuslevelScoreTemp = 0;
        GameModeManager.instance.missedNotesTemp = 0;
    }
}
