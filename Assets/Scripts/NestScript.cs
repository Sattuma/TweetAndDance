using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestScript : MonoBehaviour
{
    public int levelComplete = 0;

    public NestCollider[] nestCol= new NestCollider[6];

    public bool completeLevel_one;
    public bool completeLevel_two;
    public bool completeLevel_three;
    public bool completeLevel_four;
    public bool completeLevel_five;
    public bool completeLevel_six;

    private void Update()
    {
        if(nestCol[0].partComplete == true) { completeLevel_one = true; }
        else { completeLevel_one = false; }
        if (nestCol[1].partComplete == true) { completeLevel_two = true; }
        else { completeLevel_two = false; }
        if (nestCol[2].partComplete == true) { completeLevel_three = true; }
        else { completeLevel_three = false; }
        if (nestCol[3].partComplete == true) { completeLevel_four = true; }
        else { completeLevel_four = false; }
        if (nestCol[4].partComplete == true) { completeLevel_five = true; }
        else { completeLevel_five = false; }

        if (nestCol[nestCol.Length].partComplete == true)
        { completeLevel_six = true; }
        else { completeLevel_six = false; }
    }

    public void LevelCompleteCheck(int level)
    {
        levelComplete += level;
    }

}
