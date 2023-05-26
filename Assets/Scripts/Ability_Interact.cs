using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CLASS INCLUDE PLAYER INTERACT ABILITY AND ALL RELATED TO THAT ABILITY

public class Ability_Interact : MonoBehaviour
{
    public PlayerCore core;

    void Start()
    {
        core = GetComponent<PlayerCore>();
    }

    public void InteractActionOne()
    {
        Debug.Log("PELAAJA INTERACT ONE");
    }

    public void InteractActionTwo()
    {
        Debug.Log("PELAAJA INTERACT TWO");
    }

    public void InteractActionThree()
    {
        Debug.Log("PELAAJA INTERACT THREE");
    }
}
