using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestCollider : MonoBehaviour
{

    public NestScript nestBase;
    public HudScript hud;
    public bool partComplete;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Placeable"))
        {
            partComplete = true;
            nestBase.completeLevelFinal = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Placeable") && !hud.isCountingLevel1)
        {
            hud.StartEndGame();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Placeable"))
        {
            partComplete = false;
            nestBase.completeLevelFinal = false;
            hud.CancelEndGameHud();
        }
    }

    private void Start()
    {
        nestBase = GetComponentInParent<NestScript>();
    }

}
