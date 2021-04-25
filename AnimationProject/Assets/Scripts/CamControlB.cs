using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControlB : MonoBehaviour
{
    //Public

    public Camera playerCamera;
    public Transform targetCamera, targetCameraY, forwardPost;

    public float zoomSpeed = 150;
    public float rotationSpeed = 1.0f;
    public float aimSpeed = 50;
    private float mouseX, mouseY;

    //Private

    private float dt;

    private Vector3 vectorDir = new Vector3();
    private Vector3 vectorDirLook = new Vector3();


    // Start is called before the first frame update
    void Start()
    {
        mouseX = 0;
        mouseY = 0;
        vectorDir = (targetCamera.transform.position - playerCamera.transform.position).normalized;
        vectorDirLook = (forwardPost.transform.position - transform.position).normalized;
        vectorDirLook.y = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;

        vectorDir = (targetCamera.transform.position - playerCamera.transform.position).normalized;

        vectorDirLook = (forwardPost.transform.position - transform.position).normalized;
        vectorDirLook.y = 0;

        CameraMove();
        CameraZoom();
        Look();
    }

    private void CameraZoom()
    {
        playerCamera.transform.position += vectorDir * Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * dt;
    }

    private void CameraMove()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed * dt;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed * dt;
        mouseY = Mathf.Clamp(mouseY, -70, 70);

        //targetCameraY.rotation = Quaternion.Euler(0, mouseX, mouseY);
        targetCameraY.rotation = Quaternion.Euler(targetCameraY.rotation.x + mouseY, targetCameraY.rotation.y + mouseX, 0);
        //targetCamera.rotation = Quaternion.Euler(0, mouseX, 0);

    }

    private void Look()
    {
        if (Input.GetMouseButton((1)))
        {
            vectorDirLook = (forwardPost.transform.position - transform.position);
            vectorDirLook.y = 0;
            var rotation = Quaternion.LookRotation(vectorDirLook);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, dt * aimSpeed);
            Debug.Log("Estoy funcionando");
        }
    }
}
