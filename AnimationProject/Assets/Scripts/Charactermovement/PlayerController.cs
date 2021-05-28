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

    private GameObject objectBehindCamera;

    private float originalDistanceCam1, originalDistanceCam2;

    private Vector3 vectorDirLook = new Vector3();

    private float mouseX, mouseY;

    private bool isAlive = true;


    private void Awake()
    {
        activeCamera = playerCamera;
        myRigidbody = GetComponent<Rigidbody>();
        originalDistanceCam1 = Vector3.Distance(transform.position, playerCamera.transform.position);
        originalDistanceCam2 = Vector3.Distance(transform.position, playerAimCamera.transform.position);
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
        float maxDistance = originalDistanceCam1;
        if (activeCamera == playerAimCamera) maxDistance = originalDistanceCam2;
        float actualDistance = Vector3.Distance(transform.position, activeCamera.transform.position);
        Vector3 vectorDir = transform.position - activeCamera.transform.position;
        List<Ray> raylist = new List<Ray>();
        RaycastHit hit;

        Ray ray1 = new Ray(activeCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, -0.1f)), vectorDir);
        raylist.Add(ray1);
        Ray ray2 = new Ray(activeCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -0.1f)), vectorDir);
        raylist.Add(ray2);
        Ray ray3 = new Ray(activeCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, -0.1f)), vectorDir);
        raylist.Add(ray3);
        Ray ray4 = new Ray(activeCamera.ScreenToWorldPoint(new Vector3(0, 0, -0.1f)), vectorDir);
        raylist.Add(ray4);

        //Debug.DrawRay(activeCamera.ScreenToWorldPoint(new Vector3(0,Screen.height,-0.3f)), vectorDir, Color.red);
        //Debug.DrawRay(activeCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,-0.3f)), vectorDir, Color.red);
        //Debug.DrawRay(activeCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0,-0.3f)), vectorDir, Color.red);
        //Debug.DrawRay(activeCamera.ScreenToWorldPoint(new Vector3(0,0,-0.3f)), vectorDir, Color.red);

        for (int i = 0; i < raylist.Count; i++)
        {
            if (Physics.Raycast(raylist[i], out hit))
            {
                if (!hit.transform.gameObject.CompareTag("Player") && actualDistance > 0.25f)
                {
                    //Debug.Log("No puedo ver al jugador");
                    activeCamera.transform.position += (transform.position - activeCamera.transform.position).normalized * 3 * dt;
                }
                else if (actualDistance < maxDistance || actualDistance < 0.25f)
                {
                    Debug.Log("Puedo ver al jugador pero estoy muy cerca.");
                    activeCamera.transform.position -= (transform.position - activeCamera.transform.position).normalized * 3 * dt;
                }
                else if (actualDistance >= maxDistance)
                {
                    Debug.Log("Puedo ver al jugador a la distancia correcta.");
                    //activeCamera.transform.position = (transform.position - activeCamera.transform.position).normalized * maxDistance;
                }
            }

            Debug.Log(actualDistance);
        }
    }
    public void KillPlayer()
    {
        isAlive = false;
    }
}
