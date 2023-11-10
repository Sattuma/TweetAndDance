using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteButtonCollision : MonoBehaviour
{
    public ParticleSystem hitParticle;
    public ParticleSystem perfectParticle;

    public bool hit;
    public bool perfect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Note"))
        {
            Destroy(collision.gameObject.transform.parent.gameObject);
            GameModeManager.instance.AddBonusScore(30);
            Instantiate(hitParticle, transform.position, transform.rotation);
        }
        if (collision.gameObject.CompareTag("Perfect"))
        {
            GameModeManager.instance.AddBonusScore(100);
        }
    }

}
