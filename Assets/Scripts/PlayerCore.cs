using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// HANDLES PLAYER CURRENT STATES

public class PlayerCore : MonoBehaviour
{
    public ControlState controlState;
    public GameObject level2Pos;

    public enum ControlState
    {
        idle,
        walking,
        airborne,
    }

    public bool isGrounded;
    
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

    public void PlayerPosLevel2()
    {
        gameObject.transform.position = level2Pos.transform.position;
    }

}
