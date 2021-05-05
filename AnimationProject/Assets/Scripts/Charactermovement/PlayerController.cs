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

    public float rotationSpeed = 1.0f;
    public float aimSpeed = 50;

    public float movementSpeed = 5;

    //Private

    private float dt;

    private Rigidbody myRigidbody;

    private Camera activeCamera;

    private Vector3 vectorDirLook = new Vector3();

    private float mouseX, mouseY;


    private void Awake()
    {
        activeCamera = playerAimCamera;
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mouseX = 0;
        mouseY = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;

        CameraMove();
        Look();
        Movement();
    }

    private void FixedUpdate()
    {
        //Movement();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //transform.Translate(Vector3.forward * movementSpeed * dt);
            LookingForward();
            myRigidbody.AddForce(transform.forward * movementSpeed);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            myRigidbody.velocity = Vector3.zero;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            //transform.Translate(Vector3.back * movementSpeed * dt);
            myRigidbody.AddForce(-transform.forward * movementSpeed);
            LookingForward();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            myRigidbody.velocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(Vector3.left * movementSpeed * dt);
            myRigidbody.AddForce(-transform.right * movementSpeed);
            LookingForward();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            myRigidbody.velocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate(Vector3.right * movementSpeed * dt);
            myRigidbody.AddForce(transform.right * movementSpeed);
            LookingForward();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            myRigidbody.velocity = Vector3.zero;
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
        vectorDirLook = (forwardPost.transform.position - transform.position).normalized;
        vectorDirLook.y = 0;
        var rotation = Quaternion.LookRotation(vectorDirLook);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
        //Investigar acerca de torque
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
