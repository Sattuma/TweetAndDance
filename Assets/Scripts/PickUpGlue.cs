using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGlue : MonoBehaviour
{
    public Transform[] glueObj;
    public Transform[] attachPos;

    //bool firstAttach;
    //bool secondAttach;
    //bool thirdAttach;

    //public Transform glueChildObj;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var col = collision.gameObject.tag != "Player";

        if (collision.gameObject.tag == "Collectable" && col || collision.gameObject.tag == "NestObject" && col)
        {
            if (glueObj[0] == null)
            {
                glueObj[0] = collision.gameObject.transform;
            }
            else if (glueObj[0] != null)
            {
                if (glueObj[1] == null)
                {
                    glueObj[1] = collision.gameObject.transform;
                    glueObj[2] = null;
                }
                else if(glueObj[1] != null)
                {
                    if(glueObj[2] == null)
                    {
                        glueObj[2] = collision.gameObject.transform;
                    }
                }
            }
        }
    }

    private void Start()
    {
        //turhia? ellei käytä apuna collisionissa siisteyden vuoksi
        //firstAttach = false;
        //secondAttach = false;
        //thirdAttach = false;

        //check if obj 1,2,3 is not null nii sit attach se teittyyn paikkaan?
        //ja sille jotain paskee jo collisionissa on/off mitä tarvii?
        //KESKEN
    }

}
