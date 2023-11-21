using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGracvityPickUp : MonoBehaviour
{
    public CollectableCollision col;
    public Animator anim;
    public Rigidbody2D rb;

    public bool stopFalling;

    public LayerMask groundMask;
    public LayerMask pickUpMask;

    public float timer;
    public float force = 200;
    public float rotate = .5f;
    public float rayDetectRadius;

    private void Update()
    {
        Vector2 downRay = transform.TransformDirection(-Vector2.up) * rayDetectRadius;
        RaycastHit2D hit = Physics2D.Raycast(transform.position,-Vector2.up, rayDetectRadius, groundMask + pickUpMask);
        if (hit)
        { stopFalling = true;}
        else
        { stopFalling = false;}

        if(rb.bodyType == RigidbodyType2D.Dynamic)
        {
            if (!stopFalling)
            {
                anim.SetTrigger("Falling");
                rb.AddForce(Vector3.right * force * Time.deltaTime);
                transform.Rotate(0, 0, rotate);
                timer += Time.deltaTime;

                if (timer > 1f)
                {
                    rb.AddForce(Vector3.up * 100 * Time.deltaTime);
                    force = -force;
                    rotate = -rotate;
                    timer = 0f;
                }
            }
            else
            {
                anim.SetTrigger("FallingOff");
            }
        }
    }
}
