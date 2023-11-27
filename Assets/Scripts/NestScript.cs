using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestScript : MonoBehaviour
{
    public Animator anim;
    public float animDuration;
    public GameObject arrow1, arrow2;

    [Header("Script Variables")]
    public NestCollider1 nestCol;
    public NestCollider2 nestCol2;

    [Header("Nest Completion Booleans")]
    public bool partOneOn;
    public bool partTwoOn;

    private void Awake()
    { GameModeManager.InfoCanvasOn += StartAnim;}

    private void StartAnim()
    { StartCoroutine(NestAnimDelay()); }

    IEnumerator NestAnimDelay()
    {
        arrow1.SetActive(true);
        arrow2.SetActive(true);
        anim.SetBool("Flash", true);
        yield return new WaitForSecondsRealtime(animDuration);
        anim.SetBool("Flash", false);
        arrow1.SetActive(false);
        arrow2.SetActive(false);
    }
}
