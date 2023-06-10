using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public Ability_Movement move;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            move = collision.gameObject.GetComponent<Ability_Movement>();
            move.cantMove = true;
        }
    }

}
