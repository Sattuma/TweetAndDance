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

    public bool isUp;

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
    { target = staticObj; }

    private void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);

        movement = transform.position;

        movement.x = Mathf.Lerp(movement.x, target.position.x, cameraSpeed * Time.deltaTime);
        if (isUp)
        { movement.y = Mathf.Lerp(movement.y, target.position.y, cameraSpeed * Time.deltaTime);}
        else if(!isUp)
        { movement.y = Mathf.Lerp(movement.y, minY, cameraSpeed * Time.deltaTime); }

        transform.position = movement;
    }
    public void ChangeCamStatic()
    {
        isUp = false;
        target = staticObj;
        cameraSpeed = speedTransitionStatic;
        minY = 0;
        maxY = 25;
    }
    public void ChangeCamFollowGround()
    {
        isUp = false;
        target = followObj;
        cameraSpeed = speedTransitionFollow;
        minY = 0;
        maxY = 25;
    }

    public void ChangeCamFollowUp()
    {
        isUp = true;
        target = followObj;
        cameraSpeed = speedTransitionFollow;
        minY = 0;
        maxY = 25;
    }

    /*
    public void UpdateCamera()
    {  float size = gameObject.GetComponentInChildren<Camera>().orthographicSize;}
    */
}
