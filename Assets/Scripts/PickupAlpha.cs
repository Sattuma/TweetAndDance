using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAlpha : MonoBehaviour
{
    [Header("FADE object collision variables")]
    public SpriteRenderer imageAlpha;
    public GameObject otherTrigger;
    public float fadeOutAlpha;
    public float fadeInAlpha;
    public float fadeSpeed;

    public bool objectFading;

    private void FixedUpdate()
    {
        if(objectFading)
        {
            
        }
        else if(!objectFading)
        {
            
        }
    }

    public void FadeIn()
    {
        Debug.Log("FADE IN");
    }
    public void FadeOut()
    {
        Debug.Log("FADE OUT");
    }


}
