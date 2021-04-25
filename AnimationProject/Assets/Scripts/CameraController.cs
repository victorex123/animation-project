using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Public

    public Camera playerCamera;
    public Transform targetCamera;

    public float zoomSpeed = 150;

    //Private

    private float dt;

    private Vector3 vectorDir = new Vector3();


    // Start is called before the first frame update
    void Start()
    {
        vectorDir = (targetCamera.transform.position - playerCamera.transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;

        CameraZoom();
    }

    private void CameraZoom()
    {
        playerCamera.transform.position += vectorDir * Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * dt;
    }
}
