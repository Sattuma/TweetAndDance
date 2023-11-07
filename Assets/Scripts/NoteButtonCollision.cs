using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteButtonCollision : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Note"))
        {
            Destroy(collision.gameObject.transform.parent.gameObject);
            //pisteytys kerroin jne
        }
        if (collision.gameObject.CompareTag("Perfect"))
        {
            //perfect pisteytyts kerroin jne
        }
    }

}
