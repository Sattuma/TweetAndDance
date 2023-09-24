using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestCollider2 : MonoBehaviour
{
    public NestScript nestBase;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Placeable"))
        {
            nestBase.partTwoOn = true;
            nestBase.completeLevelFinal = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Placeable") && nestBase.partOneOn)
        {
            GameModeManager.instance.InvokeLevelCountOn();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Placeable"))
        {
            nestBase.partTwoOn = false;
            nestBase.completeLevelFinal = false;
            GameModeManager.instance.InvokeLevelCountOff();
        }
    }

    private void Start()
    { nestBase = GetComponentInParent<NestScript>();}
}
