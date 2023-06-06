using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CLASS INCLUDE PLAYER INTERACT ABILITY AND ALL RELATED TO THAT ABILITY

public class Ability_Interact : MonoBehaviour
{
    public PlayerCore core;
    public Transform dropPoint;
    public Transform collectPoint;
    public Transform collectableObj;
    public Transform collectableChildObj;
    public bool canCollect;
    public bool pickedUp;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            if(collectableObj == null)
            {
                canCollect = true;
                collectableObj = collision.gameObject.transform; // on triggerstay collectable object is collision which player collides
                collectableChildObj = collectableObj.transform.GetChild(0).gameObject.transform;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            if(canCollect)
            {
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
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1 && GameModeManager.instance.level1Over != true)
        {
            if (canCollect)
            {
                //collectableChildObj.GetComponent<BoxCollider2D>().enabled = false;
                //collectableObj.GetComponentInChildren<CapsuleCollider2D>().enabled = false;
                collectableObj.gameObject.tag = "Untagged";

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
                //collectableObj.GetComponentInChildren<CapsuleCollider2D>().enabled = true;
                //collectableObj.GetComponent<CircleCollider2D>().enabled = true;
                collectableObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                collectableObj.transform.position = dropPoint.position;
                collectableObj = null;
                pickedUp = false;
            }
        }
        else
        {
            Debug.Log("PELAAJA INTERACT ONE");
        }
    }

    public void InteractActionTwo()
    {
        Debug.Log("PELAAJA INTERACT TWO");
    }

    public void InteractActionThree()
    {
        Debug.Log("PELAAJA INTERACT THREE");
    }
}
