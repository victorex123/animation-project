using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Public

    public Camera playerCamera;
    public Transform targetCamera,targetCameraY;

    public float zoomSpeed = 150;
    public float rotationSpeed = 1.0f;
    private float mouseX, mouseY;

    //Private

    private float dt;

    private Vector3 vectorDir = new Vector3();


    // Start is called before the first frame update
    void Start()
    {
        vectorDir = (targetCamera.transform.position - playerCamera.transform.position).normalized;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;

        CameraMove();
        CameraZoom();
    }

    private void CameraZoom()
    {
        playerCamera.transform.position += vectorDir * Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * dt;
    }

    private void CameraMove()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 35);

        targetCameraY.rotation = Quaternion.Euler(0, mouseX, mouseY);
        targetCamera.rotation = Quaternion.Euler(0, mouseX, 0);

    }
}
