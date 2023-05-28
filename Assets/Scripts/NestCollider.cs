using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestCollider : MonoBehaviour
{

    public NestScript nestBase;
    public bool partComplete;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            partComplete = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            partComplete = false;

        }
    }

    private void Start()
    {
        nestBase = GetComponentInParent<NestScript>();
    }

}
