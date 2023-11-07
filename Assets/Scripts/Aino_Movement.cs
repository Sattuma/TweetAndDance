using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aino_Movement : MonoBehaviour
{

    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        GameModeManager.Fail += FailAnim;
    }

    public void FailAnim()
    {
        anim.SetTrigger("Fail");
    }
    public void WinAnim()
    {
        anim.SetTrigger("Win");
    }
}
