using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGlue : MonoBehaviour
{

    public Transform attachedObj;
    public Transform attachPos;
    public ParticleSystem attachFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var col = collision.gameObject.tag != "Player";

        if (collision.gameObject.tag == "Collectable" && col || collision.gameObject.tag == "NestObject" && col)
        {
            if (attachedObj == null && collision.gameObject.tag != "Attach")
            {
                attachedObj = collision.gameObject.transform;
                attachPos.GetComponent<FixedJoint2D>().connectedBody = attachedObj.GetComponent<Rigidbody2D>();
                Instantiate(attachFX, attachPos.position, attachPos.rotation);
            }
        }
    }
}
