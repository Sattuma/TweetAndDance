using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollider : MonoBehaviour
{
    public Ability_Movement move;

    [Header("FADE object collision variables")]
    public SpriteRenderer imageAlpha;
    public GameObject otherTrigger;
    public float fadeOutAlpha;
    public float fadeInAlpha;
    public float fadeSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shader"))
        {
            Debug.Log("Osun pensaaseen");
            AudioManager.instance.PlaySoundFX(0);
            Physics2D.IgnoreCollision(otherTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            imageAlpha = collision.gameObject.transform.GetComponent<SpriteRenderer>();
            imageAlpha.color = new Color(1, 1, 1, fadeOutAlpha);
        }

        if (collision.gameObject.CompareTag("Grass"))
        {
            Debug.Log("Osun pensaaseen");
            AudioManager.instance.PlaySoundFX(0);
        }
        /*
        if(collision.gameObject.CompareTag("LevelTwo"))
        {
            collision.gameObject.SetActive(false);
            //move.onMove = false;
        }
        */
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shader"))
        {
            Debug.Log("Pois Pensaasta");
            AudioManager.instance.PlaySoundFX(0);
            imageAlpha = collision.gameObject.transform.GetComponent<SpriteRenderer>();
            imageAlpha.color = new Color(1, 1, 1, fadeInAlpha);
        }

        if(collision.gameObject.CompareTag("Grass"))
        {
            Debug.Log("Pois Pensaasta");
            AudioManager.instance.PlaySoundFX(0);
        }
    }
    private void Start()
    {
        move = GetComponent<Ability_Movement>();
    }

}
