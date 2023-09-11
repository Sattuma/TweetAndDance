using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// HANDLES PLAYER CURRENT STATES

public class PlayerCore : MonoBehaviour
{

    public Animator myAnim;

    public bool isGrounded;
    public bool isCollecting;
    public bool isLanding;

    private void Start()
    {
        myAnim = GetComponentInChildren<Animator>();

        GameModeManager.Success += SuccessAnim;
        GameModeManager.Fail += LevelFailAnim;

        NoteScript.WrongNote += WrongNoteAnim;
        NoteScript.RightNote += RightNoteAnim;
    }

    public void JumpAnimOn()
    {
        myAnim.SetBool("JumpStart",true);
    }
    public void JumpAnimOff()
    {
        myAnim.SetBool("JumpStart", false);
    }

    public void FlyAnimOn()
    {
        myAnim.SetBool("FlyStart", true);
    }
    public void FlyAnimOff()
    {
        myAnim.SetBool("FlyStart", false);
    }

    public void LandingAnimOn()
    {
        myAnim.SetTrigger("isLanding");
    }
    public void LandingAnimOff()
    {
        myAnim.SetTrigger("LandingFinish");
    }

    public void RightNoteAnim()
    {
        //myAnim.SetTrigger("RightNote");
    }

    public void WrongNoteAnim()
    {
        //myAnim.SetTrigger("WrongNote");
    }
    public void WalkingAnim(float value)
    {
        myAnim.SetFloat("x", value);
    }

    public void SuccessAnim()
    {
        // success animaatio tähän
    }

    public void LevelFailAnim()
    {
        // fail animaatio tähän
    }

}
