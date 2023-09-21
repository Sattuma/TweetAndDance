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
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float slowedSpeed;
    //[SerializeField] private float transitionSpeed;

    void Start()
    {
        core = GetComponent<PlayerCore>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Movement(Vector2 value)
    {
        //kun enabloidaan input action movement
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel && GameModeManager.instance.levelActive == true)
        {        
            rb.velocity = new Vector2(value.x * playerSpeed, rb.velocity.y);
            rb.velocity.Normalize();
            core.WalkingAnim(value.x);

            if (value.x > 0 && !facingRight)
            { FlipCharacter(); }

            if(value.x < 0 && facingRight)
            { FlipCharacter(); }

            if(core.isLanding || core.isCollecting)
            { playerSpeed = slowedSpeed; }
            else
            { playerSpeed = walkingSpeed; }
        }

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel && GameModeManager.instance.levelActive != true)
        {
            rb.velocity = Vector2.zero;
            value.x = 0;
            //animaatio tähän?
        }
    }

    void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }



}
