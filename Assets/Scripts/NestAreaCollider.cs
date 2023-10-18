using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This Script decides whoch pick ups stays in the nest after level is ending (IF THIS IS NECESSARY)
public class NestAreaCollider : MonoBehaviour
{

    public Transform nestObj;
    public Transform childObj;
    public Transform childOfChildObj;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Collectable"))
        {
            //collision.gameObject.tag = "NestObject";
            nestObj = collision.gameObject.transform;
            childObj = nestObj.gameObject.transform.GetChild(0).gameObject.transform;
            childOfChildObj = childObj.gameObject.transform.GetChild(0).gameObject.transform;
            

            if (childOfChildObj.CompareTag("Secret"))
            {
                childOfChildObj.tag = "SecretFound";
            }
            else
            {
                nestObj.tag = "NestObject";
            }
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
