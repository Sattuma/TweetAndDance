using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePickUp : MonoBehaviour
{

    public ParticleSystem collectFx;
    public ParticleSystem secondsFx;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(collectFx, gameObject.transform.position, gameObject.transform.rotation);
            Instantiate(secondsFx, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);

            GameModeManager.instance.AddTime(30);

        }
    }

}
