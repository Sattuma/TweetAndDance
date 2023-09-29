using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CLASS INCLUDE PLAYER AIRBORNE ABILITY AND ALL RELATED TO THAT ABILITY

public class Ability_Airborne : MonoBehaviour
{



    public PlayerCore core;
    public Rigidbody2D rb;

    public GameObject groundCheckObj;
    public GameObject landingCheckObj;
    public float jumpPower = 250f;
    public float flyPower = 80f;
    public float flyingGravityRising;
    public float flyingGravityFalling;
    public float fallMultiplier = 80f;
    public float lowJumpMultiplier = 80f;
    public float groundDetectRadius = 1f;
    public float y;
    public LayerMask groundMask;

    public bool jumpIsPressed;

    void Start()
    {
        core = GetComponent<PlayerCore>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GroundCheck();
        GravityCheck();
    }

    void GroundCheck()
    {
        core.isGrounded = (Physics2D.OverlapCircle(groundCheckObj.transform.position, groundDetectRadius, groundMask)&& rb.velocity.y <= 0);
        core.myAnim.SetFloat("y", rb.velocity.y);

        if (rb.velocity.y < 0 && !core.isGrounded)
        {
            rb.gravityScale = flyingGravityFalling;
            core.myAnim.SetBool("OnGround", false);
        }
        if(core.isGrounded)
        {
            rb.gravityScale = 1f;
            core.myAnim.SetBool("OnGround", true);
            landingCheckObj.GetComponent<BoxCollider2D>().enabled = true;
        }
        if(!core.isGrounded)
        {
            landingCheckObj.GetComponent<BoxCollider2D>().enabled = false;
        }
        
    }

    public void GravityCheck()
    {
        y = rb.velocity.y;

        // PLAYER GRAVITY MULTIPLIES DURING FALLING
        if (y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
        }
        // IF JUMP BUTTON IS RELEASED DURING JUMP, PLAYER FALL DOWN FASTER AND GET SMALLER JUMP
        else if (y > 0 && !jumpIsPressed)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
        }

    }

    public void JumpAction()
    {
        if(GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel && GameModeManager.instance.levelActive == true)
        {
            if (core.isGrounded == true)
            {
                AudioManager.instance.PlaySoundFX(2);
                //rb.velocity = Vector2.up * jumpPower;
                rb.AddForce(transform.up * jumpPower);
                core.JumpAnimOn();
                
                
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void FlyAction()
    {
        if(GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel && GameModeManager.instance.levelActive == true)
        {
            
            if (core.isGrounded == false)
            {
                rb.velocity = Vector2.up * flyPower;
                //rb.AddForce(transform.up * flyPower);
                core.FlyAnimOn();
                core.myAnim.SetFloat("y", rb.velocity.y);
                rb.gravityScale = flyingGravityRising;
            }
        }

    }
}
