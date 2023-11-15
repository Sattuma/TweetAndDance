using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStateCheck : MonoBehaviour
{

    public GameObject colHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Early"))
        {
            colHit.GetComponent<NoteButtonCollision>().isEarly = true;
        }
        if (collision.gameObject.CompareTag("Perfect"))
        {
            colHit.GetComponent<NoteButtonCollision>().isPerfect = true;
        }
        if (collision.gameObject.CompareTag("Late"))
        {
            colHit.GetComponent<NoteButtonCollision>().isLate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Early"))
        {
            colHit.GetComponent<NoteButtonCollision>().isEarly = false;
        }
        if (collision.gameObject.CompareTag("Perfect"))
        {
            colHit.GetComponent<NoteButtonCollision>().isPerfect = false;
        }
        if (collision.gameObject.CompareTag("Late"))
        {
            colHit.GetComponent<NoteButtonCollision>().isLate = false;
        }
    }

}
