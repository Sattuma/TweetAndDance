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
        GroundCheck();
    }

    void GroundCheck()
    {
       core.isGrounded = Physics2D.OverlapCircle
            (groundCheckObj.transform.position, groundDetectRadius, groundMask);

        if(rb.velocity.y > 0 && !core.isGrounded)
        {
            rb.gravityScale = 0.5f;
            core.AirborneStatus();
            core.myAnim.SetFloat("y", rb.velocity.y);
        }
        else if(rb.velocity.y < 0 && !core.isGrounded)
        {
            rb.gravityScale = 1f;
        }

        if(core.isGrounded)
        {
            rb.gravityScale = 1f;
            core.myAnim.SetBool("OnGround", true);
            core.myAnim.SetBool("Reset", true);
        }
        else if (!core.isGrounded)
        {
            core.myAnim.SetBool("OnGround", false);
            core.myAnim.SetBool("Reset", false);
        }

    }

    public void JumpAction()
    {
        if(GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1 && GameModeManager.instance.levelActive == true)
        {
            if (core.isGrounded == true)
            {
                rb.AddForce(transform.up * jumpPower);
                core.myAnim.SetTrigger("JumpStart");
                core.myAnim.SetBool("Reset", false);

            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }

    public void FlyAction()
    {
        if(GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1)
        {
            if (core.isGrounded == false)
            {
                rb.AddForce(transform.up * flyPower);
                if (rb.velocity.y <= -0.5)
                {
                    rb.AddForce(transform.up * jumpPower);
                }

                core.myAnim.SetFloat("y", rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }

}
