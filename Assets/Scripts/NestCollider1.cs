using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestCollider1 : MonoBehaviour
{
    public NestScript nestBase;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Placeable"))
        { nestBase.partOneOn = true;}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Placeable"))
        { nestBase.partOneOn = false;}
    }
    private void Start()
    { nestBase = GetComponentInParent<NestScript>();}
}
