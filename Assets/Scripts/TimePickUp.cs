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

            float normal = GameModeManager.instance.timerNormalMode;
            float hard = GameModeManager.instance.timerHardMode;

            normal += 50;
            hard += 50;
            Debug.Log(normal);

            /*
            float normalMax = PlayerPrefs.GetFloat("TimerNormal");
            float hardMax = PlayerPrefs.GetFloat("TimerHard");

            if(normal >= normalMax)
            {
                GameModeManager.instance.timerNormalMode = normalMax;
            }
            if (hard >= hardMax)
            {
                GameModeManager.instance.timerHardMode = hardMax;
            }
            */
        }
    }

}
