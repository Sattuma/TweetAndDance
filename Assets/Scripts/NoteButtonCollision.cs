using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteButtonCollision : MonoBehaviour
{
    public ParticleSystem hitParticle;
    public ParticleSystem perfectParticle;

    public bool isEarly;
    public bool isPerfect;
    public bool isLate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Note"))
        {


            if (isEarly && !isPerfect && !isLate)
            {
                GameModeManager.instance.AddBonusScore(120);
                Instantiate(hitParticle, transform.position, transform.rotation);
                Destroy(collision.gameObject.transform.parent.gameObject);
            }
            if (isEarly && isPerfect && !isLate)
            {
                GameModeManager.instance.AddBonusScore(200);
                Instantiate(perfectParticle, transform.position, transform.rotation);
                Destroy(collision.gameObject.transform.parent.gameObject);
            }
            if(!isEarly && isPerfect && isLate)
            {
                GameModeManager.instance.AddBonusScore(120);
                Instantiate(hitParticle, transform.position, transform.rotation);
                Destroy(collision.gameObject.transform.parent.gameObject);
            }



        }
    }

}
