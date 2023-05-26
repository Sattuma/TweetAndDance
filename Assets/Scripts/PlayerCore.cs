using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// HANDLES PLAYER CURRENT STATES

public class PlayerCore : MonoBehaviour
{
    //public static delegate Event StateCheck();

    public Rigidbody2D rb;
    public ControlState controlState;

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

}
