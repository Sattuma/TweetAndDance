using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// CLASS INCLUDE PLAYER MOVEMENT ABILITY AND ALL RELATED TO THAT ABILITY

public class Ability_Movement : MonoBehaviour
{

    public PlayerCore core;
    public Rigidbody2D rb;
    public bool facingRight;

    [SerializeField] private float playerSpeed;

    void Start()
    {
        core = GetComponent<PlayerCore>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Movement(Vector2 value)
    {
        //enabloidaan input action movement
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1)
        {
            rb.velocity = new Vector2(value.x * playerSpeed, rb.velocity.y);
            rb.velocity.Normalize();

            if(value.x > 0 && !facingRight)
            {
                FlipCharacter();
            }
            if(value.x < 0 && facingRight)
            {
                FlipCharacter();
            }

            if (core.isGrounded)
            {
                core.myAnim.SetFloat("x", value.x);
            }

            if ((rb.velocity.x > 0 || rb.velocity.x < 0) && core.isGrounded)
            { core.WalkingStatus(); }

            else if (rb.velocity.x == 0 && core.isGrounded)
            { core.IdleStatus(); }
        }

        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }



}
