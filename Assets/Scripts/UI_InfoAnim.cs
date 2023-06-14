using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InfoAnim : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetAnimationInactive()
    {
        gameObject.SetActive(false);
    }
}
