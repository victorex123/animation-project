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

    public GameObject testCube;

    //Private

    private float dt;

    private Rigidbody myRigidbody;

    private Camera activeCamera;

    private float playerCameraOriginalDistance;
    private float playerAimCameraOriginalDistance;

    private float playerCameraActualDistance;
    private float playerAimCameraActualDistance;

    private GameObject objectBehindCamera;

    private Vector3 vectorDirLook = new Vector3();

    private float mouseX, mouseY;

    private bool isAlive = true;


    private void Awake()
    {
        activeCamera = playerCamera;
        myRigidbody = GetComponent<Rigidbody>();

        playerCameraOriginalDistance = Vector3.Distance(transform.position, playerCamera.transform.position);
        playerAimCameraOriginalDistance = Vector3.Distance(playerAimCamera.transform.position, transform.position);

        playerCameraActualDistance = playerCameraOriginalDistance;
        playerAimCameraActualDistance = playerAimCameraOriginalDistance;
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
            CameraAdjustDistance();
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
        else if (Input.GetMouseButtonUp(1))
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
    }

    private void CameraChange(bool cam1, bool cam2)
    {
        playerCamera.gameObject.SetActive(cam1);
        playerAimCamera.gameObject.SetActive(cam2);

        if (cam1) activeCamera = playerCamera;
        if (cam2) activeCamera = playerAimCamera;

    }
    private void CameraAdjustDistance()
    {
        RaycastHit hit, backHit;
        Ray ray = activeCamera.ScreenPointToRay(activeCamera.WorldToScreenPoint(transform.position));
        Ray rayBack = new Ray(activeCamera.transform.position, (activeCamera.transform.position - transform.position).normalized * 0.5f);

        Debug.DrawRay(activeCamera.transform.position, transform.position-activeCamera.transform.position, Color.green);
        Debug.DrawRay(activeCamera.transform.position, (activeCamera.transform.position-transform.position).normalized * 0.5f, Color.red);

        if (Physics.Raycast(rayBack, out backHit))
        {
            objectBehindCamera = backHit.transform.gameObject;
            Instantiate(testCube, backHit.point, Quaternion.identity);

        }

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            if (objectHit.CompareTag("Player"))
            {
                //Debug.Log("Estoy viendo al jugador.");
                if (activeCamera == playerCamera && (playerCameraActualDistance - dt * 0.5f) <= playerCameraOriginalDistance)
                {
                    activeCamera.transform.position -= (transform.position - activeCamera.transform.position).normalized * dt * 0.5f;
                }

                if (activeCamera == playerAimCamera && (playerAimCameraActualDistance - dt * 0.5f) < playerAimCameraOriginalDistance)
                {
                    activeCamera.transform.position -= (transform.position - activeCamera.transform.position).normalized * dt * 0.5f;
                }

                if (activeCamera == playerCamera)
                {
                    playerCameraActualDistance = Vector3.Distance(transform.position, activeCamera.transform.position);
                }

                if (activeCamera == playerAimCamera)
                {
                    playerAimCameraActualDistance = Vector3.Distance(transform.position, activeCamera.transform.position);
                }

                //Debug.Log("Distancia actual: " + playerCameraActualDistance + ", Distancia máxima: " + playerCameraOriginalDistance);
            }
            else
            {
                //Debug.Log("No estoy viendo al jugador.");
                activeCamera.transform.position += (transform.position - activeCamera.transform.position).normalized * dt * 10f;
                if (activeCamera == playerCamera) playerCameraActualDistance = Vector3.Distance(transform.position, activeCamera.transform.position);
                if (activeCamera == playerAimCamera) playerAimCameraActualDistance = Vector3.Distance(transform.position, activeCamera.transform.position);
            }
        }
    }
    private void CameraHitWall()
    { 

    }

    public void killPlayer()
    {
        isAlive = false;
    }
}
