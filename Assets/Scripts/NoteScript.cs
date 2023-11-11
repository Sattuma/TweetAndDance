using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    public float currentSpeed;
    public Rigidbody2D rb;

    public ParticleSystem deadZoneHitFX;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -currentSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Goal"))
        {
            Instantiate(deadZoneHitFX, transform.position, transform.rotation);
            GameModeManager.instance.AddBonusScore(-50);
            Destroy(gameObject);
        }
    }

}
