using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGlue : MonoBehaviour
{

    public Transform selfObj;

    public Transform attachedObj;
    public Transform attachPos;
    public ParticleSystem attachFX;

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
                attachPos.GetComponent<FixedJoint2D>().connectedBody = attachedObj.GetComponent<Rigidbody2D>();
                Instantiate(attachFX, attachPos.position, attachPos.rotation);
                attachedObj.GetComponent<CircleCollider2D>().enabled = false;
                Physics2D.IgnoreCollision(selfObj.GetComponent<Collider2D>(), colGlueChild.GetComponent<Collider2D>());
            }
        }
    }
}
