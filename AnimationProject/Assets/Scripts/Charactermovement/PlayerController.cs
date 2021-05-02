using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Public 
    public Camera playerCamera;
    public Camera playerAimCamera;

    public GameObject pivotPoint;

    public Transform targetCamera, targetCameraY, forwardPost;

    public float zoomSpeed = 150;
    public float rotationSpeed = 1.0f;
    public float aimSpeed = 50;

    public float movementSpeed = 5;

    //Private

    private float dt;

    private Camera activeCamera;

    private Vector3 vectorDir = new Vector3();
    private Vector3 vectorDirLook = new Vector3();

    private float mouseX, mouseY;


    private void Awake()
    {
        activeCamera = playerAimCamera;
    }

    // Start is called before the first frame update
    void Start()
    {
        mouseX = 0;
        mouseY = 0;
        vectorDir = (targetCamera.transform.position - activeCamera.transform.position).normalized;
        vectorDirLook = (forwardPost.transform.position - transform.position).normalized;
        vectorDirLook.y = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;

        vectorDir = (targetCamera.transform.position - activeCamera.transform.position).normalized;

        vectorDirLook = (forwardPost.transform.position - transform.position).normalized;
        vectorDirLook.y = 0;

        CameraMove();
        Look();
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            LookingForward();
            transform.position += vectorDirLook * 1 * dt;
        }
    }
    private void CameraMove()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed * dt;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed * dt;
        mouseY = Mathf.Clamp(mouseY, -30, 20);

        pivotPoint.transform.rotation = Quaternion.Euler(pivotPoint.transform.rotation.x + mouseY, pivotPoint.transform.rotation.y + mouseX, 0);

    }
    private void Look()
    {
        if (Input.GetMouseButton((1)))
        {
            CameraChange(false,true);
            LookingForward();
        }
        else
        {
            CameraChange(true, false);
        }
    }

    private void LookingForward()
    {
        vectorDirLook = (forwardPost.transform.position - transform.position);
        vectorDirLook.y = 0;
        var rotation = Quaternion.LookRotation(vectorDirLook);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, dt * aimSpeed);
    }

    private void CameraChange(bool cam1, bool cam2)
    {
        playerCamera.gameObject.SetActive(cam1);
        playerAimCamera.gameObject.SetActive(cam2);

        if (activeCamera == playerCamera)
        {
            activeCamera = playerAimCamera;
        }
        else
        {
            activeCamera = playerCamera;
        }

    }
}
