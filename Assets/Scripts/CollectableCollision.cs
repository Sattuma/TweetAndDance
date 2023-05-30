using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCollision : MonoBehaviour
{
    public GameObject childObject;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            gameObject.tag = "Collectable";
            childObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

}
