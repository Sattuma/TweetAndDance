using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// CLASS INCLUDE PLAYER MOVEMENT ABILITY AND ALL RELATED TO THAT ABILITY

public class Ability_Movement : MonoBehaviour
{

    public PlayerCore core;
    public Rigidbody2D rb;

    [SerializeField] private float playerSpeed;

    void Start()
    {
        core = GetComponent<PlayerCore>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Movement(Vector2 value)
    {
        rb.velocity = new Vector2(value.x * playerSpeed, rb.velocity.y);
        rb.velocity.Normalize();

        if ((rb.velocity.x > 0 || rb.velocity.x < 0) && core.isGrounded)
        { core.WalkingStatus(); }

        else if (rb.velocity.x == 0 && core.isGrounded)
        { core.IdleStatus(); }

    }
}
