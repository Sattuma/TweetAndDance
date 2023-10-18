using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCollision : MonoBehaviour
{

    [SerializeField] private Renderer myModelParent;
    [SerializeField] private Renderer myModel;

    public GameObject childObject;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            gameObject.tag = "Collectable";
            childObject.tag = "Placeable";
            childObject.GetComponent<CapsuleCollider2D>().enabled = true;
        }

    }
    private void Awake()
    {
        HighLightOff();
    }
    public void HighLightOn()
    { myModel.gameObject.SetActive(true);}

    public void HighLightOff()
    { myModel.gameObject.SetActive(false);}
}
