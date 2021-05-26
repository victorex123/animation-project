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

    public float movementSpeed = 50;
    public float maxMovementSpeed = 5;

    //Private

    private float dt;

    private Rigidbody myRigidbody;

    private Camera activeCamera;

    private Vector3 vectorDirLook = new Vector3();

    private float mouseX, mouseY;

    private bool isAlive = true;


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

        if (isAlive)
        {
            CameraMove();
            Look();
            if (myRigidbody.velocity.y > -0.5f && myRigidbody.velocity.y < 0.5f)
            {
                Movement();
            }
        }

    }

    private void FixedUpdate()
    {
        //Movement();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.W)) myRigidbody.AddForce(transform.forward * movementSpeed);


            if (Input.GetKey(KeyCode.S)) myRigidbody.AddForce(-transform.forward * movementSpeed);


            if (Input.GetKey(KeyCode.A)) myRigidbody.AddForce(-transform.right * movementSpeed);


            if (Input.GetKey(KeyCode.D)) myRigidbody.AddForce(transform.right * movementSpeed);


            if (myRigidbody.velocity.magnitude > maxMovementSpeed)
            {
                myRigidbody.velocity = myRigidbody.velocity.normalized * maxMovementSpeed;
            }
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

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            LookingForward();
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
    public void killPlayer()
    {
        isAlive = false;
    }
}
