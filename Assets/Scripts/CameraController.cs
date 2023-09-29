using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Transform followObj;
    public Transform staticObj;
    public Transform leftObj;
    public Transform rightObj;

    //VARIABLE FOR CAMERA POSITION VALUES
    public Vector2 movement;
    public float cameraOffsetY = 3;

    //VARIABLES FOR CAMERA SPEED
    public float cameraSpeed;
    public float speedTransitionStatic;
    public float speedTransitionFollow;
    public float cameraSpeedY;

    //VARIABLES FOR CAMERA MOVEMENT LIMITATION
    public float minY;
    public float maxY;
    public float minX;
    public float maxX;


    private void Start()
    {
        target = staticObj;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);


        movement = transform.position;
        //x axis camera follow
        movement.x = Mathf.Lerp(movement.x, target.position.x, cameraSpeed * Time.deltaTime);


        transform.position = movement;

        /*
        if(target = followObj)
        {
            UpdateCamera();
        }
        */
    }

    public void ChangeCamStatic()
    {
        target = staticObj;
        cameraSpeed = speedTransitionStatic;
        //gameObject.GetComponentInChildren<Camera>().orthographicSize = 8;
    }
    public void ChangeCamFollow()
    {
        target = followObj;
        cameraSpeed = speedTransitionFollow;
        //gameObject.GetComponentInChildren<Camera>().orthographicSize = 9;
    }

    public void UpdateCamera()
    {
        float size = gameObject.GetComponentInChildren<Camera>().orthographicSize;
        //size = new (Mathf.Clamp(size, 8,9), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);

    }
}
