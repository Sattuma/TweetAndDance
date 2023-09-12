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
    {
        //materiaali koko muutos
        myModel.gameObject.SetActive(true);
        /*
        //Materiaali väri muutos
        Color color = myModel.material.color;
        color.a = 255f;
        myModel.material.color = Color.black;
        myModel.material.color = color;
        Debug.Log("HIGHLIGHT PICKUP ON");
        */
    }

    public void HighLightOff()
    {
        //materiaali koko muutos
        myModel.gameObject.SetActive(false);
        /*
        //Materiaali väri muutos
        Color color = myModel.material.color;
        color.a = 0f;
        myModel.material.color = Color.black;
        myModel.material.color = color;
        Debug.Log("HIGHLIGHT PICKUP OFF");
        */
    }
}
