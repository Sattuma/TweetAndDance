using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// CLASS INCLUDE PLAYER MOVEMENT ABILITY AND ALL RELATED TO THAT ABILITY

public class Ability_Movement : MonoBehaviour
{

    public PlayerCore core;
    public Rigidbody2D rb;

    public bool facingRight;
    public bool playFootsteps;

    //PlayerSpeed
    [SerializeField] private float playerSpeed;
    //PlayerSpeed Variables
    [SerializeField] private float acceleration;
    [SerializeField] private float deacceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float slowedSpeed;

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
            CalculateSpeed(value);
            rb.velocity = new Vector2(value.x * playerSpeed, rb.velocity.y);
            rb.velocity.Normalize();
            core.WalkingAnim(value.x);

            if (value.x > 0 && core.isGrounded && !core.isLanding || value.x < 0 && core.isGrounded && !core.isLanding)
            {
                playFootsteps = true;
                ActivateFootstepsFX();
            }
            else
            {
                playFootsteps = false;
                ActivateFootstepsFX();
            }

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

    public void CalculateSpeed(Vector2 speed)
    {

        if (speed.x > 0 || speed.x < 0)
        { walkingSpeed += acceleration * Time.deltaTime; }
        else
        { walkingSpeed -= deacceleration * Time.deltaTime; }
        walkingSpeed = Mathf.Clamp(walkingSpeed, 0, maxSpeed);
    }

    void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void ActivateFootstepsFX()
    {
        if (playFootsteps)
        { AudioManager.instance.movementFXSource.GetComponent<AudioSource>().enabled = true;}
        else
        { AudioManager.instance.movementFXSource.GetComponent<AudioSource>().enabled = false;}
    }

}
