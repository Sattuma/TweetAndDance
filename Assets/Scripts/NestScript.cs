using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestScript : MonoBehaviour
{
    public NestCollider nestCol;
    public bool completeLevelFinal;

    private void Update()
    {
        if(nestCol.partComplete == true)
        {
            completeLevelFinal = true;

        }
        else
        {
            completeLevelFinal = false;
        }
    }
}
