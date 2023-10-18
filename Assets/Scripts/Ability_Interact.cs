using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CLASS INCLUDE PLAYER INTERACT ABILITY AND ALL RELATED TO THAT ABILITY

public class Ability_Interact : MonoBehaviour
{
    [Header("Level 1 Actions")]
    public PlayerCore core;
    public Transform dropPoint;
    public Transform collectPoint;
    public Transform collectableObj;
    public Transform collectableChildObj;
    public bool canCollect;
    public bool pickedUp;

    [Header("Level 2 Actions")]
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

    //[Header("Level 3 Actions")]
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable") || collision.gameObject.CompareTag("NestObject"))
        {
            if(collectableObj == null)
            {
                canCollect = true;
                collectableObj = collision.gameObject.transform; // on triggerstay collectable object is collision which player collides
                collectableChildObj = collectableObj.transform.GetChild(0).gameObject.transform;
                collectableObj.GetComponent<CollectableCollision>().HighLightOn();

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable") || collision.gameObject.CompareTag("NestObject"))
        {
            if(canCollect)
            {
                collectableObj.GetComponent<CollectableCollision>().HighLightOff();
                canCollect = false;
                collectableObj = null;
                collectableChildObj = null;
                
            }
        }
    }

    void Start()
    {
        core = GetComponent<PlayerCore>();
        collectableObj = null;
    }


    public void InteractActionOne()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel && GameModeManager.instance.levelActive == true)
        {

            if (canCollect)
            {
                collectableObj.GetComponent<CollectableCollision>().HighLightOff();
                StartCoroutine(CollectDelay());
                collectableObj.GetComponentInChildren<CapsuleCollider2D>().enabled = false;
                collectableObj.gameObject.tag = "Untagged";
                collectableChildObj.gameObject.tag = "Untagged";
                core.myAnim.SetTrigger("Pickup");
                collectableObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                collectableObj.gameObject.transform.parent = transform; // if canCollect bool is active when collect button is pressed, player pick that collectable
                collectableObj.transform.position = collectPoint.transform.position;
                canCollect = false;
                pickedUp = true;
            }
            else if (pickedUp)
            {
                collectableObj.gameObject.transform.parent = null; // if collectable is already picked up in Collect button is pressed again, player drops the collectable object
                collectableObj.GetComponentInChildren<CapsuleCollider2D>().enabled = true;
                collectableObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                collectableObj.transform.position = dropPoint.position;
                collectableObj = null;
                pickedUp = false;
            }
        }
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel)
        {
            button1.GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    public void InteractActionTwo()
    {

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel)
        {
            button2.GetComponent<CircleCollider2D>().enabled = true;
        }

    }

    public void InteractActionThree()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel)
        {
            button3.GetComponent<CircleCollider2D>().enabled = true;
        }

    }

    public void InteractActionCancel()
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel)
        {
            button1.GetComponent<CircleCollider2D>().enabled = false;
            button2.GetComponent<CircleCollider2D>().enabled = false;
            button3.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    public IEnumerator CollectDelay()
    {
        core.isCollecting = true;
        yield return new WaitForSecondsRealtime(0.3f);
        core.isCollecting = false;
    }
}
