using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGracvityPickUp : MonoBehaviour
{
    public ParticleSystem objAppearFX;
    public CollectableCollision col;



    private void Awake()
    {
        if(!col.isLanded)
        {
            Instantiate(objAppearFX, transform.position, transform.rotation);
        }
    }


}
