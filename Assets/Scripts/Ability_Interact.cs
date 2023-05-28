using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CLASS INCLUDE PLAYER INTERACT ABILITY AND ALL RELATED TO THAT ABILITY

public class Ability_Interact : MonoBehaviour
{
    public PlayerCore core;
    public Transform dropPoint;
    public Transform collectableObj;
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
        if(canCollect)
        {
            collectableObj.GetComponent<BoxCollider2D>().enabled = false;
            collectableObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            collectableObj.gameObject.transform.parent = transform; // if canCollect bool is active when collect button is pressed, player pick that collectable
            collectableObj.transform.position = gameObject.transform.position;
            canCollect = false;
            pickedUp = true;
        }
        else if(pickedUp)
        {
            collectableObj.gameObject.transform.parent = null; // if collectable is already picked up in Collect button is pressed again, player drops the collectable object
            collectableObj.GetComponent<BoxCollider2D>().enabled = true;
            collectableObj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            collectableObj.transform.position = dropPoint.position;
            collectableObj = null;
            pickedUp = false;
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
