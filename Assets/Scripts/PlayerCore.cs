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
        myAnim.SetTrigger("WinTrig");
    }

    public void LevelFail()
    {
        myAnim.SetTrigger("LoseTrig");
    }



}
