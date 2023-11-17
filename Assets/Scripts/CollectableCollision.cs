using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCollision : MonoBehaviour
{

    [SerializeField] private Renderer myModelParent;
    [SerializeField] private Renderer myModel;

    public ParticleSystem landingFX;
    public bool isLanded;

    public GameObject childObject;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        isLanded = true;

        if (collision.gameObject.tag != "Collectable")
        {
            Instantiate(landingFX, transform.position, transform.rotation);
            landingFX.Play();
        }



        if(collision.gameObject.tag != "Player")
        {
            gameObject.tag = "Collectable";
            childObject.tag = "Placeable";
            childObject.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isLanded = false;
    }
    private void Awake()
    { HighLightOff();}

    public void HighLightOn()
    { myModel.gameObject.SetActive(true);}

    public void HighLightOff()
    { myModel.gameObject.SetActive(false);}
}
