using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
