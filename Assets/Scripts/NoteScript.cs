using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    public delegate void SingAnim();
    public static event SingAnim RightNote;
    public static event SingAnim WrongNote;

    public float currentSpeed;
    public float[] speed = new float[5];

    public bool isEarly;
    public bool isPerfect;
    public bool isLate;

    public ParticleSystem earlyPointFX;
    public ParticleSystem latePointFX;
    public ParticleSystem perfectPointFX;

    public PlayerCore core;

    public Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Note"))
        {
            if(isEarly && isPerfect)
            {
                GameModeManager.instance.AddScore(10);
                Instantiate(earlyPointFX, transform.position, transform.rotation);
                Destroy(gameObject,0.2f);
                GameModeManager.instance.scoreEndCount += 1;
                RightNote?.Invoke();
            }

            if (isLate && isPerfect)
            {
                GameModeManager.instance.AddScore(10);
                Instantiate(latePointFX, transform.position, transform.rotation);
                Destroy(gameObject, 0.2f);
                GameModeManager.instance.scoreEndCount += 1;

                RightNote?.Invoke();

            }
            if (isPerfect)
            {
                GameModeManager.instance.AddScore(50);
                Instantiate(perfectPointFX, transform.position, transform.rotation);
                Destroy(gameObject,0.2f);
                GameModeManager.instance.scoreEndCount += 1;

                RightNote?.Invoke();
;
            }

            if (isEarly && !isPerfect || isLate && !isPerfect)
            {

                Destroy(gameObject);
                WrongNote?.Invoke();

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
    }



}
