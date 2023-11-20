using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGracvityPickUp : MonoBehaviour
{
    public CollectableCollision col;
    public bool stopFalling;
    public float radius;
    public GameObject groundCheckObj;
    public LayerMask groundMask;
    public LayerMask pickUpMask;
    public Animator anim;
    public Rigidbody2D rb;
    public float timer;
    public float force = 200;
    public float rotate = .5f;

    private void Start()
    {
        //StartCoroutine(Falling());
    }

    private void Update()
    {
        stopFalling = (Physics2D.OverlapCircle(groundCheckObj.transform.position, radius,groundMask));
        stopFalling = (Physics2D.OverlapCircle(groundCheckObj.transform.position, radius,pickUpMask));

        /*
        if(!stopFalling)
        {
            anim.SetTrigger("Falling");
        }
        else if(stopFalling)
        {
            anim.SetTrigger("FallingOff");
        }
        */

        if(!stopFalling)
        {
            //lisää animaatioita myös lisäksi. muokkaa
            //anim.SetTrigger("Falling");
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
            //lisää animaatioita myös lisäksi. muokkaa
            //anim.SetTrigger("FallingOff");
        }
    }
}
