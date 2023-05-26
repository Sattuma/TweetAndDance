using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CLASS INCLUDE PLAYER AIRBORNE ABILITY AND ALL RELATED TO THAT ABILITY

public class Ability_Airborne : MonoBehaviour
{
    public PlayerCore core;
    public Rigidbody2D rb;

    public GameObject groundCheckObj;
    public float jumpPower = 250f;
    public float flyPower = 80f;
    public float groundDetectRadius = 1f;
    public LayerMask groundMask;

    void Start()
    {
        core = GetComponent<PlayerCore>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log(rb.velocity.y);
        GroundCheck();
    }

    void GroundCheck()
    {
        core.isGrounded = Physics2D.OverlapCircle
            (groundCheckObj.transform.position, groundDetectRadius, groundMask);

        if(rb.velocity.y > 0 && !core.isGrounded)
        {
            rb.gravityScale = 0.5f;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    public void JumpAction()
    {
        if(core.isGrounded)
        {
            rb.AddForce(transform.up * jumpPower);
            Debug.Log("PELAAJA HYPPÄÄ");
        }
    }

    public void FlyAction()
    {
        if(!core.isGrounded)
        {
            rb.AddForce(transform.up * flyPower);
            if(rb.velocity.y <= -0.5)
            {
                rb.AddForce(transform.up * jumpPower);
            }
            Debug.Log("PELAAJA LENTÄÄ");
        }
    }
}
