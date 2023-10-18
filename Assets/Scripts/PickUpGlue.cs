using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGlue : MonoBehaviour
{
    public Transform glueObj;
    public Transform glueChildObj;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Secret") && collision.gameObject.tag != "Player")
        {
            if (glueObj == null)
            {

                glueObj = collision.gameObject.transform;
                glueChildObj = glueObj.transform.GetChild(0).gameObject.transform;
                glueChildObj.transform.parent = gameObject.transform;
            }
        }


    }


}
