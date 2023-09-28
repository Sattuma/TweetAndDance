using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTriggers : MonoBehaviour
{
    [SerializeField]private CinemachineVirtualCamera staticCam;
    [SerializeField]private CinemachineVirtualCamera followCam;


    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
