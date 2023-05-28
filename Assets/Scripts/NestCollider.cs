using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestCollider : MonoBehaviour
{

    public NestScript nestBase;
    public bool partComplete;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Placeable"))
        {
            partComplete = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Placeable") && partComplete == false)
        {
            nestBase.LevelCompleteCheck(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Placeable"))
        {
            partComplete = false;

        }
    }

    private void Start()
    {
        nestBase = GetComponentInParent<NestScript>();
    }

}
