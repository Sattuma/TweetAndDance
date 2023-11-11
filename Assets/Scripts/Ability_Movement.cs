using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// CLASS INCLUDE PLAYER MOVEMENT ABILITY AND ALL RELATED TO THAT ABILITY

public class Ability_Movement : MonoBehaviour
{
    [Header("Components")]
    public PlayerCore core;
    public Rigidbody2D rb;

    [Header("Booleans")]
    public bool facingRight;
    public bool playFootsteps;

    [Header("Player Movement")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float movementSmooth;

    [Header("Variables for Movement")]
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float slowedSpeed;
    [SerializeField] private Vector2 velocity = Vector2.zero;

    void Start()
    {
        core = GetComponent<PlayerCore>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Movement(float value)
    {
        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.gameLevel && GameModeManager.instance.levelActive == true)
        {
            if(core.isGrounded)
            {
                Vector2 targetVelocity = new Vector2(value * playerSpeed, rb.velocity.y);
                rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmooth);
                core.dustFX.gameObject.SetActive(true);
                core.flyTrail.gameObject.SetActive(false);

            }
            else if(!core.isGrounded)
            {
                rb.velocity = new Vector2(value * playerSpeed, rb.velocity.y);
                core.dustFX.gameObject.SetActive(false);
                core.flyTrail.gameObject.SetActive(true);
            }

            rb.velocity.Normalize();
            core.WalkingAnim(value);

            if (value > 0 && core.isGrounded && !core.isLanding || value < 0 && core.isGrounded && !core.isLanding)
            {
                playFootsteps = true;
                ActivateFootstepsFX();
                core.PLayDust();
            }
            else
            {
                playFootsteps = false;
                ActivateFootstepsFX();
            }

            if (value > 0 && !facingRight)
            { FlipCharacter(); }

            if(value < 0 && facingRight)
            { FlipCharacter(); }

            if(core.isLanding || core.isCollecting)
            { playerSpeed = slowedSpeed; }
            else
            { playerSpeed = walkingSpeed; }
        }

        if (GameModeManager.instance.activeGameMode == GameModeManager.GameMode.bonusLevel && GameModeManager.instance.levelActive != true)
        {
            rb.velocity = Vector2.zero;
            value = 0;
            //animaatio tähän?
        }
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
