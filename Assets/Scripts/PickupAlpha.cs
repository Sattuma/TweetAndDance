using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAlpha : MonoBehaviour
{

    public Animator anim;
    public SpriteRenderer imageAlpha;

    //public float fadeOutAlpha;
    //public float fadeInAlpha;
    //public float fadeSpeed;

    //public bool objectFading;
    //public bool stopFadeAction;

    private void Start()
    {
        //stopFadeAction = false;
    }

    public void FadeInAnim()
    {
        anim.SetTrigger("FadeIn");
    }
    public void FadeOutAnim()
    {
        anim.SetTrigger("FadeOut");
    }

    /*
    private void Update()
    {
        if(objectFading && !stopFadeAction)
        {
            FadeIn();
        }
        else if(!objectFading && stopFadeAction)
        {
            FadeOut();
        }
    }
    */



    /*
    public void FadeIn()
    {
        imageAlpha.gameObject.transform.GetComponent<SpriteRenderer>();
        imageAlpha.color = new Color(1, 1, 1, fadeOutAlpha);
        StartCoroutine(FadeDelay());
    }
    public void FadeOut()
    {
        imageAlpha.gameObject.transform.GetComponent<SpriteRenderer>();
        imageAlpha.color = new Color(1, 1, 1, fadeInAlpha);
        StartCoroutine(FadeDelay());
    }

    IEnumerator FadeDelay()
    {
        yield return new WaitForSecondsRealtime(.7f);
        objectFading = false;
        stopFadeAction = false;


    }
    */

}
