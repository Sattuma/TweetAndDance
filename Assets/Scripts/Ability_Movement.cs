using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// CLASS INCLUDE PLAYER MOVEMENT ABILITY AND ALL RELATED TO THAT ABILITY

public class Ability_Movement : MonoBehaviour
{

    public PlayerCore core;
    public Rigidbody2D rb;
    public bool facingRight;
    public bool onMove;
    public GameObject level2Pos;


    [SerializeField] private float playerSpeed;
    [SerializeField] private float transitionSpeed;

    private void Awake()
    {
        GameModeManager.Level1End += FlipInCutScene2;
    }

    void Start()
    {
        core = GetComponent<PlayerCore>();
        rb = GetComponent<Rigidbody2D>();
        level2Pos.SetActive(false);
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

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.level2 && GameModeManager.instance.levelActive != true)
        {
            rb.velocity = Vector2.zero;
            value.x = 0;
            core.myAnim.SetFloat("x", value.x);
            PlayerPosLevel2();

        }
    }

    void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    //LEVEL 2 CUTSCENE
    public void PlayerPosLevel2()
    {
        if(onMove)
        {
            core.myAnim.SetTrigger("JumpStart");
            transform.position = Vector3.Lerp(transform.position, level2Pos.transform.position, transitionSpeed * Time.deltaTime);
        }
        else
        {
            core.myAnim.SetBool("Reset", true);
            rb.velocity = Vector2.zero;
            rb.gravityScale = 15;
            if(facingRight)
            {
                FlipCharacter();
            }
        }
    }
    public void FlipInCutScene2()
    {
        if(!facingRight && GameModeManager.instance.level2Retry != true)
        {
            FlipCharacter();
        }

    }


}
