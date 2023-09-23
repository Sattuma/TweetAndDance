using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public Ability_Movement move;
    public Renderer renderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shader"))
        {
            renderer = collision.gameObject.GetComponent<Renderer>();
            Color color = renderer.material.color;
            color.a = 100;

        }

        /*
        if(collision.gameObject.CompareTag("LevelTwo"))
        {
            collision.gameObject.SetActive(false);
            //move.onMove = false;
        }
        */
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shader"))
        {
            renderer = collision.gameObject.GetComponent<Renderer>();
            Color color = renderer.material.color;
            color.a = 255;
        }
    }
    private void Start()
    {
        move = GetComponent<Ability_Movement>();
    }

}
