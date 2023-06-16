using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AinoSpawner : MonoBehaviour
{

    public GameObject ainoPrefab;
    public Animator anim;

    public GameObject startPosLevel2, endPosLevel2, startPosLevel3, endPosLevel3;

    public bool onMove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Aino"))
        {
            
        }
    }

    private void Start()
    {
        anim = ainoPrefab.GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if(onMove)
        {
            MoveToEndPosLevel2();
        }
    }


    public void MoveToEndPosLevel2()
    {
        anim.SetTrigger("FlyDown");
        ainoPrefab.transform.position = Vector3.Lerp(ainoPrefab.transform.position, endPosLevel2.transform.position, 1f * Time.deltaTime);
    }
    
    public void StopMovement()
    {
        anim.SetTrigger("Reset");
    }

    public void SpawnAinoLevel3()
    {

    }
}
