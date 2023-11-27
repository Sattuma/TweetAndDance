using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollider : MonoBehaviour
{
    public GameObject infoCanvas;
    public InfoCanvas infoScript;

    public ParticleSystem grassAppearFX;
    public ParticleSystem leafAppearFX;
   // public SpriteRenderer imageAlpha;
    public GameObject landingTrigger;
    //public float fadeOutAlpha;
    //public float fadeInAlpha;
    //public float fadeSpeed;

    //public bool objectFading;


    private void Start()
    {
        infoCanvas = GameObject.FindGameObjectWithTag("InfoCanvas");
        infoScript = infoCanvas.GetComponent<InfoCanvas>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // COLLISION WHERE ASSETS TURNS TRANSPARENT WHEN PLAYER COLLIDES
        if (collision.gameObject.CompareTag("Shader"))
        {
            Physics2D.IgnoreCollision(landingTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            collision.gameObject.GetComponentInParent<PickupAlpha>().FadeOutAnim();
        }

        // GAME ASSETS WITH TAG ELEMNTS WHERE PLAYER COLLIDE
        if (collision.gameObject.CompareTag("Grass"))
        {
            Physics2D.IgnoreCollision(landingTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            AudioManager.instance.PlaySoundFX(0);
            Instantiate(grassAppearFX, transform.position, transform.rotation);
        }
        if (collision.gameObject.CompareTag("TreeLeaves"))
        {
            Physics2D.IgnoreCollision(landingTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            AudioManager.instance.PlaySoundFX(0);
            Instantiate(leafAppearFX, transform.position, transform.rotation);
        }

        //CAMERA TRIGGERS WHERE PLAYER COLLIDE
        if (collision.gameObject.CompareTag("StaticCamTrig"))
        {
            Physics2D.IgnoreCollision(landingTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            FindCameraStatic();

            //old system where camera follows always player when level starts
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

        //DIRECTIONAL ARROWN FOR UI TRIGGERS WHEN PLAYER COLLIDE

        if (collision.gameObject.CompareTag("CenterGroundDir") && GameModeManager.instance.levelActive)
        {
            infoScript.arrowLeft.SetActive(true);
            infoScript.arrowRight.SetActive(true);
            infoScript.arrowUp.SetActive(true);
            infoScript.arrowDown.SetActive(false);

        }
        if (collision.gameObject.CompareTag("LeftGroundDir"))
        {
            infoScript.arrowUp.SetActive(true);
            infoScript.arrowDown.SetActive(false);
            infoScript.arrowRight.SetActive(true);
            infoScript.arrowLeft.SetActive(false);
        }
        if (collision.gameObject.CompareTag("RightGroundDir"))
        {
            infoScript.arrowUp.SetActive(true);
            infoScript.arrowDown.SetActive(false);
            infoScript.arrowLeft.SetActive(true);
            infoScript.arrowRight.SetActive(false);
        }
        if (collision.gameObject.CompareTag("CenterAirDir"))
        {
            infoScript.arrowLeft.SetActive(true);
            infoScript.arrowRight.SetActive(true);
            infoScript.arrowDown.SetActive(true);
            infoScript.arrowUp.SetActive(false);
        }
        if (collision.gameObject.CompareTag("LeftAirDir"))
        {
            infoScript.arrowLeft.SetActive(false);
            infoScript.arrowRight.SetActive(true);
            infoScript.arrowDown.SetActive(true);
            infoScript.arrowUp.SetActive(false);
        }
        if (collision.gameObject.CompareTag("RightAirDir"))
        {
            infoScript.arrowLeft.SetActive(true);
            infoScript.arrowRight.SetActive(false);
            infoScript.arrowDown.SetActive(true);
            infoScript.arrowUp.SetActive(false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // COLLISION WHERE ASSETS TURNS TRANSPARENT WHEN PLAYER EXITS
        if (collision.gameObject.CompareTag("Shader"))
        {
            Physics2D.IgnoreCollision(landingTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            collision.gameObject.GetComponentInParent<PickupAlpha>().FadeInAnim();
        }

        // GAME ASSETS WITH TAG ELEMNTS WHERE PLAYER EXITS
        if (collision.gameObject.CompareTag("Grass"))
        {
            Physics2D.IgnoreCollision(landingTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            AudioManager.instance.PlaySoundFX(0);
            Instantiate(grassAppearFX, transform.position, transform.rotation);
        }
        if (collision.gameObject.CompareTag("TreeLeaves"))
        {
            Physics2D.IgnoreCollision(landingTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            AudioManager.instance.PlaySoundFX(0);
            Instantiate(leafAppearFX, transform.position, transform.rotation);
        }

        //DIRECTIONAL ARROWN FOR UI TRIGGERS WHEN PLAYER EXITS

        if (collision.gameObject.CompareTag("CenterGroundDir"))
        {

        }
        if (collision.gameObject.CompareTag("LeftGroundDir"))
        {

        }
        if (collision.gameObject.CompareTag("RightGroundDir"))
        {

        }
        if (collision.gameObject.CompareTag("CenterAirDir"))
        {

        }
        if (collision.gameObject.CompareTag("LeftAirDir"))
        {

        }
        if (collision.gameObject.CompareTag("RightAirDir"))
        {

        }
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
