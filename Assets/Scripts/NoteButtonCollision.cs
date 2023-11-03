using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteButtonCollision : MonoBehaviour
{


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Early"))
        {
            NoteScript note = collision.gameObject.GetComponentInParent<NoteScript>();
            note.isEarly = true;
        }
        if (collision.gameObject.CompareTag("Perfect"))
        {
            NoteScript note = collision.gameObject.GetComponent<NoteScript>();
            note.isPerfect = true;
        }
        if (collision.gameObject.CompareTag("Late"))
        {
            NoteScript note = collision.gameObject.GetComponentInParent<NoteScript>();
            note.isLate = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Early"))
        {
            NoteScript note = collision.gameObject.GetComponentInParent<NoteScript>();
            note.isEarly = true;
        }
        if (collision.gameObject.CompareTag("Perfect"))
        {
            NoteScript note = collision.gameObject.GetComponent<NoteScript>();
            note.isPerfect = true;
        }
        if (collision.gameObject.CompareTag("Late"))
        {
            NoteScript note = collision.gameObject.GetComponentInParent<NoteScript>();
            note.isLate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Early"))
        {
            NoteScript note = collision.gameObject.GetComponentInParent<NoteScript>();
            note.isEarly = false;
        }
        if (collision.gameObject.CompareTag("Perfect"))
        {
            NoteScript note = collision.gameObject.GetComponent<NoteScript>();
            note.isPerfect = false;
        }
        if (collision.gameObject.CompareTag("Late"))
        {
            NoteScript note = collision.gameObject.GetComponentInParent<NoteScript>();
            note.isLate = false;
        }
    }

    private void Start()
    {
        
    }

}
