using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// CLASS INCLUDE PLAYER MOVEMENT ABILITY AND ALL RELATED TO THAT ABILITY

public class Ability_Movement : MonoBehaviour
{

    public PlayerCore core;
    public Rigidbody2D rb;
    public bool facingRight;
    public bool cantMove;
    public GameObject level2Pos;

    Vector2 movement;

    [SerializeField] private float playerSpeed;

    void Start()
    {
        core = GetComponent<PlayerCore>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Movement(Vector2 value)
    {
        //enabloidaan input action movement
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level1 && GameModeManager.instance.levelActive == true)
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

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level2 && GameModeManager.instance.levelActive == true)
        {

            rb.velocity = Vector2.zero;
            value.x = 0;
            core.myAnim.SetFloat("x", value.x);
            if (!cantMove)
            {
                PlayerPosLevel2();
            }
            else
            {
                StopLevel2Pos();
                if(facingRight)
                {
                    facingRight = !facingRight;
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }

            }

        }
    }

    void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    //LEVEL 2 POSITION CHANGE
    public void PlayerPosLevel2()
    {
        Vector2 direction = level2Pos.transform.position - transform.position;
        direction.Normalize();
        movement = direction;
        MoveLevel2Pos2(movement);
    }
    public void MoveLevel2Pos2(Vector2 direction)
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        core.myAnim.SetBool("JumpStart", true);
        rb.MovePosition((Vector2)transform.position + (playerSpeed* 2 * Time.deltaTime * direction));
    }
    public void StopLevel2Pos()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        core.myAnim.SetBool("Reset",true);
        rb.gravityScale = 50;
        rb.velocity = Vector2.zero;
    }

}
