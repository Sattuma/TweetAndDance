using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollider : MonoBehaviour
{
    public Ability_Movement move;
    public ParticleSystem grassAppearFX;
    public ParticleSystem leafAppearFX;

    [Header("FADE object collision variables")]
   // public SpriteRenderer imageAlpha;
    public GameObject landingTrigger;
    //public float fadeOutAlpha;
    //public float fadeInAlpha;
    //public float fadeSpeed;

    //public bool objectFading;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shader"))
        {
            AudioManager.instance.PlaySoundFX(0);
            //Physics2D.IgnoreCollision(otherTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            //imageAlpha = collision.gameObject.transform.GetComponent<SpriteRenderer>();
            //imageAlpha.color = new Color(1, 1, 1, fadeOutAlpha);
            //objectFading = true;

            Physics2D.IgnoreCollision(landingTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            collision.gameObject.GetComponentInParent<PickupAlpha>().FadeOutAnim();

            //collision.gameObject.GetComponentInParent<PickupAlpha>().objectFading = true;
            //collision.gameObject.GetComponentInParent<PickupAlpha>().stopFadeAction = false;
        }

        if (collision.gameObject.CompareTag("Grass"))
        {
            AudioManager.instance.PlaySoundFX(0);
            Instantiate(grassAppearFX, transform.position, transform.rotation);
        }
        if (collision.gameObject.CompareTag("TreeLeaves"))
        {
            AudioManager.instance.PlaySoundFX(0);
            Instantiate(leafAppearFX, transform.position, transform.rotation);
        }

        if (collision.gameObject.CompareTag("StaticCamTrig"))
        {
            Physics2D.IgnoreCollision(landingTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            FindCameraStatic();
            //StartCoroutine(SwitchCameraTarget());
        }

        if (collision.gameObject.CompareTag("FollowCamTrig"))
        {
            Physics2D.IgnoreCollision(landingTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            FindCameraUp();
        }

        if (collision.gameObject.CompareTag("LeftCamTrig"))
        {
            Physics2D.IgnoreCollision(landingTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            FindCameraLeft();
        }

        if (collision.gameObject.CompareTag("RightCamTrig"))
        {
            Physics2D.IgnoreCollision(landingTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            FindCameraRight();
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

            //imageAlpha = collision.gameObject.transform.GetComponent<SpriteRenderer>();
            //imageAlpha.color = new Color(1, 1, 1, fadeInAlpha);
            //objectFading = false;

            Physics2D.IgnoreCollision(landingTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            collision.gameObject.GetComponentInParent<PickupAlpha>().FadeInAnim();

            //collision.gameObject.GetComponentInParent<PickupAlpha>().objectFading = false;
            //collision.gameObject.GetComponentInParent<PickupAlpha>().stopFadeAction = true;
        }

        if(collision.gameObject.CompareTag("Grass"))
        {
            AudioManager.instance.PlaySoundFX(0);
            Instantiate(grassAppearFX, transform.position, transform.rotation);
        }
        if (collision.gameObject.CompareTag("TreeLeaves"))
        {
            AudioManager.instance.PlaySoundFX(0);
            Instantiate(leafAppearFX, transform.position, transform.rotation);
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
        //controller.target = controller.staticObj;
        controller.ChangeCamStatic();
    }

    public void FindCameraUp()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController controller = camera.GetComponent<CameraController>();
        //controller.target = controller.leftObj;
        controller.ChangeCamFollowUp();
    }
    public void FindCameraLeft()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController controller = camera.GetComponent<CameraController>();
        //controller.target = controller.leftObj;
        controller.ChangeCamFollowGround();
    }
    public void FindCameraRight()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController controller = camera.GetComponent<CameraController>();
        //controller.target = controller.rightObj;
        controller.ChangeCamFollowGround();
    }
    
    
    IEnumerator SwitchCameraTarget()
    {
        yield return new WaitUntil(() => GameModeManager.instance.levelActive);
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController controller = camera.GetComponent<CameraController>();
        controller.target = controller.followObj;
    }

}
