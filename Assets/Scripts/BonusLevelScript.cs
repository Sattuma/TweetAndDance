using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelScript : MonoBehaviour
{
    private void Awake()
    {

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
        GameModeManager.instance.BonusLevelActive();
    }
}
