using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public Ability_Movement move;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("LevelTwo"))
        {
            
            collision.gameObject.SetActive(false);
            move.onMove = false;

        }
    }

    private void Start()
    {
        move = GetComponent<Ability_Movement>();
    }

}
