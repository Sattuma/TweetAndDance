using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackGround : MonoBehaviour
{
    public Transform target;

    public float minX;
    public float maxX;
    public float offset;

    public float scrollSpeed;

    void Update()
    {
        transform.position = new Vector3(target.position.x / scrollSpeed - offset, transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);

    }
}
