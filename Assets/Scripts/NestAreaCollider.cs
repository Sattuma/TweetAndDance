using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This Script decides whoch pick ups stays in the nest after level is ending (IF THIS IS NECESSARY)
public class NestAreaCollider : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Collectable"))
        {
            collision.gameObject.tag = "NestObject";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NestObject"))
        {
            collision.gameObject.tag = "Collectable";
        }
    }
}
