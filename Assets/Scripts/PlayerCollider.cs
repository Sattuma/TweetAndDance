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

    public bool objectFading;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shader"))
        {
            AudioManager.instance.PlaySoundFX(0);
            Physics2D.IgnoreCollision(otherTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            imageAlpha = collision.gameObject.transform.GetComponent<SpriteRenderer>();
            imageAlpha.color = new Color(1, 1, 1, fadeOutAlpha);
            objectFading = true;
        }

        if (collision.gameObject.CompareTag("Grass"))
        {
            AudioManager.instance.PlaySoundFX(0);
        }

        if (collision.gameObject.CompareTag("LeftCamTrig"))
        {
            Physics2D.IgnoreCollision(otherTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            //FindCameraLeft();
        }
        if (collision.gameObject.CompareTag("StaticCamTrig"))
        {
            Physics2D.IgnoreCollision(otherTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            FindCameraStatic();
            StartCoroutine(SwitchCameraTarget());
        }
        if (collision.gameObject.CompareTag("RightCamTrig"))
        {
            Physics2D.IgnoreCollision(otherTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            //FindCameraRight();
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
            AudioManager.instance.PlaySoundFX(0);
            imageAlpha = collision.gameObject.transform.GetComponent<SpriteRenderer>();
            imageAlpha.color = new Color(1, 1, 1, fadeInAlpha);
            objectFading = false;
        }

        if(collision.gameObject.CompareTag("Grass"))
        {
            AudioManager.instance.PlaySoundFX(0);
        }
    }
    private void Start()
    {
        move = GetComponent<Ability_Movement>();
    }

    public void FindCameraStatic()
    {

        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController controller = camera.GetComponent<CameraController>();
        controller.target = controller.staticObj;
    }

    
    public void FindCameraLeft()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController controller = camera.GetComponent<CameraController>();
        controller.target = controller.leftObj;
    }
    public void FindCameraRight()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController controller = camera.GetComponent<CameraController>();
        controller.target = controller.rightObj;
    }
    
    
    IEnumerator SwitchCameraTarget()
    {
        yield return new WaitUntil(() => GameModeManager.instance.levelActive);
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController controller = camera.GetComponent<CameraController>();
        controller.target = controller.followObj;
    }

}
