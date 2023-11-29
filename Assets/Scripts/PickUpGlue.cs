using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGlue : MonoBehaviour
{

    public Transform selfObj;
    public Transform playerObj;

    public Transform attachedObj;
    public Transform attachPos;
    public ParticleSystem attachFX;

    public bool isAttached;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var colPlayer = collision.gameObject.tag != "Player";

        if (collision.gameObject.tag == "Collectable" && colPlayer || collision.gameObject.tag == "NestObject" && colPlayer)
        {
            var colGlue = collision.gameObject.transform.GetChild(0);
            var colGlueChild = colGlue.GetChild(0);

            if (attachedObj == null && colGlueChild.gameObject.tag != "Attach")
            {
                attachedObj = collision.gameObject.transform;

                attachedObj.parent = attachPos.transform;

                attachedObj.GetComponent<CircleCollider2D>().enabled = false;
                attachedObj.GetComponent<Rigidbody2D>().gravityScale = 0;

                playerObj = GameObject.FindGameObjectWithTag("Player").transform; 

                Physics2D.IgnoreCollision(selfObj.GetComponent<Collider2D>(), colGlueChild.GetComponent<Collider2D>());
                attachPos.gameObject.GetComponent<Collider2D>().enabled = true;

                Physics2D.IgnoreCollision(colGlueChild.GetComponent<Collider2D>(), attachPos.GetComponent<Collider2D>());


                isAttached = true;



                //attachPos.GetComponent<FixedJoint2D>().connectedBody = attachedObj.GetComponent<Rigidbody2D>();

                Instantiate(attachFX, attachPos.position, attachPos.rotation);

                //

                //Physics2D.IgnoreCollision(selfObj.GetComponent<Collider2D>(), colGlueChild.GetComponent<Collider2D>());
            }
        }

    }

    private void FixedUpdate()
    {
        if(isAttached)
        {
            UpdateObjPosition();
        }
    }
    void UpdateObjPosition()
    {

        attachedObj.position = attachPos.position;
        attachedObj.rotation = attachPos.rotation;

        Physics2D.IgnoreCollision(playerObj.GetComponent<Collider2D>(), attachPos.GetComponent<Collider2D>());


    }
}
