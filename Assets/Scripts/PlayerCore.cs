using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// HANDLES PLAYER CURRENT STATES

public class PlayerCore : MonoBehaviour
{

    public ParticleSystem dustFX;
    public ParticleSystem dustGrassFX;
    public ParticleSystem dustRockFX;
    public ParticleSystem dustWoodFX;

    public TrailRenderer flyTrail;

    public Animator myAnim;

    public bool isGrounded;
    public bool isCollecting;
    public bool isLanding;

    private void Start()
    {
        myAnim = GetComponentInChildren<Animator>();

        //EVENT CALL TO FUNCTIONS
        GameModeManager.Success += SuccessAnim;
        GameModeManager.Fail += LevelFailAnim;

        GameModeManager.BonusOneStart += BonusOneTransitionAnim;
        GameModeManager.BonusOneEnd += BonusOneEndAnim;
        GameModeManager.BonusSuccess += SuccessAnim;
        GameModeManager.BonusFail += LevelFailAnim;
    }

    //-------------------------------------

    //PLAYER ACTION ANIMATION TRIGGER FUNCTIONS
    public void JumpAnimOn()
    { myAnim.SetBool("JumpStart",true);}
    public void JumpAnimOff()
    { myAnim.SetBool("JumpStart", false);}
    public void FlyAnimOn()
    { myAnim.SetBool("FlyStart", true);}
    public void FlyAnimOff()
    { myAnim.SetBool("FlyStart", false);}
    public void LandingAnimOn()
    { myAnim.SetTrigger("isLanding");}
    public void LandingAnimOff()
    { myAnim.SetTrigger("LandingFinish");}
    public void WalkingAnim(float value)
    { myAnim.SetFloat("x", value);}

    public void BonusOneTransitionAnim()
    { myAnim.SetTrigger("Bonus1"); }
    public void BonusOneEndAnim()
    { myAnim.SetTrigger("BonusEnd"); }

    //-------------------------------------

    //END GAME ANIMATION TRIGGER FUNCTIONS
    public void SuccessAnim()
    {
        if(isGrounded)
        { myAnim.SetTrigger("WinTrig");}
        else
        { StartCoroutine(SuccessAnimDelay());}
    }
    IEnumerator SuccessAnimDelay()
    {
        yield return new WaitUntil(() => isLanding);
        myAnim.SetTrigger("WinTrig");
    }
    public void LevelFailAnim()
    {
        if (isGrounded)
        { myAnim.SetTrigger("LoseTrig");}
        else
        { StartCoroutine(FailAnimDelay());}
    }
    IEnumerator FailAnimDelay()
    {
        yield return new WaitUntil(() => isLanding);
        myAnim.SetTrigger("LoseTrig");
    }

    //-------------------------------------

    //kutsu bonsukentän animaatioita eventtikutsussa. tehty bonsukenttien alkuun event

    //BONUSLEVEL ANIMATION TRIGGER FUNCTIONS
    public void RightNoteAnim()
    {//myAnim.SetTrigger("RightNote");
    }

    public void WrongNoteAnim()
    {//myAnim.SetTrigger("WrongNote");
    }
    public void PLayDust()
    {
        dustFX.Play();
    }

    public void PLayDustGrass()
    {
        dustGrassFX.Play();
    }
    public void PLayDustRock()
    {
        dustRockFX.Play();
    }
    public void PLayDustWood()
    {
        dustWoodFX.Play();
    }
}
