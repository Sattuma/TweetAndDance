using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollider : MonoBehaviour
{
    public ParticleSystem grassAppearFX;
    public ParticleSystem leafAppearFX;
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
            Physics2D.IgnoreCollision(landingTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            collision.gameObject.GetComponentInParent<PickupAlpha>().FadeOutAnim();
        }

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
            Physics2D.IgnoreCollision(landingTrigger.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
            collision.gameObject.GetComponentInParent<PickupAlpha>().FadeInAnim();
        }

        if(collision.gameObject.CompareTag("Grass"))
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
    }

    public void FindCameraStatic()
    {
        //Change Camera
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController controller = camera.GetComponent<CameraController>();
        //controller.target = controller.staticObj;
        controller.ChangeCamStatic();

        //Change InfoCanvas
        GameObject infoUi = GameObject.FindGameObjectWithTag("InfoCanvas");
        InfoCanvas infoCanvasScript = infoUi.GetComponent<InfoCanvas>();
        infoCanvasScript.arrowLeft.SetActive(true);
        infoCanvasScript.arrowRight.SetActive(true);
        infoCanvasScript.arrowUp.SetActive(true);
    }

    public void FindCameraUp()
    {
        //Change Camera
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController controller = camera.GetComponent<CameraController>();
        //controller.target = controller.leftObj;
        controller.ChangeCamFollowUp();
        //Change InfoCanvas
        GameObject infoUi = GameObject.FindGameObjectWithTag("InfoCanvas");
        InfoCanvas infoCanvasScript = infoUi.GetComponent<InfoCanvas>();
        infoCanvasScript.arrowLeft.SetActive(false);
        infoCanvasScript.arrowRight.SetActive(false);
        infoCanvasScript.arrowUp.SetActive(false);
    }

    public void FindCameraLeft()
    {
        //Change Camera 
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController controller = camera.GetComponent<CameraController>();
        //controller.target = controller.leftObj;
        controller.ChangeCamFollowGround();

        //Change InfoCanvas
        GameObject infoUi = GameObject.FindGameObjectWithTag("InfoCanvas");
        InfoCanvas infoCanvasScript = infoUi.GetComponent<InfoCanvas>();
        infoCanvasScript.arrowLeft.SetActive(false);
    }
    public void FindCameraRight()
    {
        //Change Camera
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController controller = camera.GetComponent<CameraController>();
        //controller.target = controller.rightObj;
        controller.ChangeCamFollowGround();

        //Change InfoCanvas
        GameObject infoUi = GameObject.FindGameObjectWithTag("InfoCanvas");
        InfoCanvas infoCanvasScript = infoUi.GetComponent<InfoCanvas>();
        infoCanvasScript.arrowRight.SetActive(false);
    }


    IEnumerator SwitchCameraTarget()
    {
        yield return new WaitUntil(() => GameModeManager.instance.levelActive);
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController controller = camera.GetComponent<CameraController>();
        controller.target = controller.followObj;
    }

}
