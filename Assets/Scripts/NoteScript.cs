using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    public float currentSpeed;
    public float[] speed = new float[5];

    public bool isEarly;
    public bool isPerfect;
    public bool isLate;

    public ParticleSystem earlyPointFX;
    public ParticleSystem latePointFX;
    public ParticleSystem perfectPointFX;

    public Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Note"))
        {
            if(isEarly && isPerfect)
            {
                GameModeManager.instance.AddScore(100);
                Instantiate(earlyPointFX, transform.position, transform.rotation);
                Destroy(gameObject);
                GameModeManager.instance.scoreEndCount += 1;
            }

            if (isLate && isPerfect)
            {
                GameModeManager.instance.AddScore(100);
                Instantiate(latePointFX, transform.position, transform.rotation);
                Destroy(gameObject);
                GameModeManager.instance.scoreEndCount += 1;
            }
            if (isEarly || isLate)
            {

            }
            if(isPerfect)
            {
                GameModeManager.instance.AddScore(500);
                Instantiate(perfectPointFX, transform.position, transform.rotation);
                Destroy(gameObject);
                GameModeManager.instance.scoreEndCount += 1;
            }
            
        }
        if (collision.gameObject.CompareTag("Goal"))
        {
            Destroy(gameObject);
            GameModeManager.instance.scoreEndCount += 1;
        }
    }
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = (speed[Random.Range(0, 5)]);
        rb.velocity = new Vector2(-currentSpeed , rb.velocity.y);
        //transform.Translate(Vector2.left * currentSpeed);
    }

}
