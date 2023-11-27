using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCanvas : MonoBehaviour
{
    public GameObject arrowLeft, arrowRight, arrowUp,arrowDown;

    private void Start()
    {
        GameModeManager.InfoCanvasOn += DirectionArrows;
        GameModeManager.Success += CanvasOff;
        GameModeManager.Fail += CanvasOff;

    }

    public void DirectionArrows()
    {
        int levelIndex = GameModeManager.instance.levelIndex;
        var mode = GameModeManager.instance.activeGameMode;
        var gameLevel = GameModeManager.GameMode.gameLevel;

        if (mode == gameLevel && levelIndex == 1)
        {
            arrowLeft.SetActive(true);
            arrowRight.SetActive(true);
        }
        else if (mode == gameLevel && levelIndex > 1)
        {
            arrowLeft.SetActive(true);
            arrowRight.SetActive(true);
            arrowUp.SetActive(true);
        }
    }

    public void CanvasOff()
    {
        arrowLeft.SetActive(false);
        arrowRight.SetActive(false);
        arrowUp.SetActive(false);
    }

}
