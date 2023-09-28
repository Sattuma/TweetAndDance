using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

    public static CinemachineVirtualCamera ActiveCam = null;

    public static void Register(CinemachineVirtualCamera camera)
    {

    }

    public static void UnRegister(CinemachineVirtualCamera camera)
    {

    }
}
