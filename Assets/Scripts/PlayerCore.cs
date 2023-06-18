using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// HANDLES PLAYER CURRENT STATES

public class PlayerCore : MonoBehaviour
{

    public ControlState controlState;

    public Animator myAnim;

    private void Start()
    {
        myAnim = GetComponentInChildren<Animator>();
        GameModeManager.Success += Success;
        GameModeManager.Fail += LevelFail;
        NoteScript.WrongNote += WrongNoteAnim;
        NoteScript.RightNote += RightNoteAnim;
    }

    public enum ControlState
    {
        idle,
        walking,
        airborne,
        landing
    }

    public bool isGrounded;
    
    //PLAYERSTATUS
    public void IdleStatus()
    {
        controlState = PlayerCore.ControlState.idle;
    }
    public void WalkingStatus()
    {
        controlState = PlayerCore.ControlState.walking;
    }
    public void AirborneStatus()
    {
        controlState = PlayerCore.ControlState.airborne;
    }
    public void LandingStatus()
    {
        controlState = PlayerCore.ControlState.landing;
    }

    public void Success()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1)
        {
            myAnim.SetTrigger("WinTrig");
        }

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level2)
        {
            myAnim.SetTrigger("WinTrig2");
        }
    }

    public void RightNoteAnim()
    {
        myAnim.SetTrigger("RightNote");
    }

    public void WrongNoteAnim()
    {
        myAnim.SetTrigger("WrongNote");
    }

    public void LevelFail()
    {
        if(GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1)
        {
            myAnim.SetTrigger("LoseTrig");
        }

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level2)
        {
            myAnim.SetTrigger("LoseTrig2");
        }

    }



}
