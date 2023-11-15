using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NoteScript : MonoBehaviour
{

    public float currentSpeed;
    public Rigidbody2D rb;

    public ParticleSystem deadZoneHitFX;

    public Sprite[] noteImage;
    public SpriteRenderer spriteToShow;

    private void Start()
    {
        spriteToShow.sprite = noteImage[Random.Range(0, noteImage.Length)];
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -currentSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Goal"))
        {
            Instantiate(deadZoneHitFX, transform.position, transform.rotation);
            GameModeManager.instance.AddBonusScore(-30);
            Destroy(gameObject);
        }
    }

}
