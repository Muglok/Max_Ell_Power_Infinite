using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera myCamera;
    public GameObject follow;
    public Vector2 minCamPos, maxCamPos;
    public float smoothTime;
    public bool active = false;
    public bool ready = false;

    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = transform.GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        if(active)
        {
            Resize();
        }
        if(ready)
        {
            Follow();
        }
    }

    public void Resize()
    {
        float cameraZoom = 5f;

        float cameraZoomDiference = cameraZoom - myCamera.orthographicSize;
        float cameraZoomSpeed = 2.8f;

        myCamera.orthographicSize += cameraZoomDiference * cameraZoomSpeed * Time.deltaTime;

       
        float posX = Mathf.SmoothDamp(transform.position.x,
        follow.transform.position.x, ref velocity.x, 0.7f);
        float posY = Mathf.SmoothDamp(transform.position.y,
            follow.transform.position.y, ref velocity.y, 0.7f);

        transform.position = new Vector3(
            Mathf.Clamp(posX, minCamPos.x, maxCamPos.x),
            Mathf.Clamp(posY, minCamPos.y, maxCamPos.y),
            transform.position.z);

        if (cameraZoomDiference >= -0.1f)
        {
            myCamera.orthographicSize = 5;
            active = false;
            ready = true;
            follow.SendMessage("IsGameStatePlaying");
        }
    }

    public void Follow()
    {
        float posX = Mathf.SmoothDamp(transform.position.x,
            follow.transform.position.x, ref velocity.x, smoothTime);
        float posY = Mathf.SmoothDamp(transform.position.y,
            follow.transform.position.y, ref velocity.y, smoothTime);

        transform.position = new Vector3(
            Mathf.Clamp(posX, minCamPos.x, maxCamPos.x),
            Mathf.Clamp(posY, minCamPos.y, maxCamPos.y),
            transform.position.z);
    }

    public void IsActive()
    {
        active = true;
    }
}
