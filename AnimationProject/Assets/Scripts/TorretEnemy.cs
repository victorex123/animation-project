using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretEnemy : MonoBehaviour
{
    private Rigidbody rigibody;
    public float maxTimeToUp = 5.0f;
    public float time = 0;
    public float maxTimeToDown = 0.0f;
    public bool upTorret = false;
    public bool downTorret = false;
    public float forceToApply = 5.0f;
    private GameObject player;
    public float accelerationAngular = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigibody = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RotateTorret();

        if (upTorret)
        {
            if (time >= maxTimeToUp)
            {
                upTorret = false;
                rigibody.velocity = Vector3.zero;
                
            }
            else 
            {
                time += Time.deltaTime;
                UpTorret();
            }
        }

        if(downTorret)
        {
            if (time <= maxTimeToDown)
            {
                downTorret = false;
                rigibody.velocity = Vector3.zero;
            }
            else
            {
                time -= Time.deltaTime;
                DownTorret();
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rigibody.velocity = Vector3.zero;
            upTorret = true;
            downTorret = false; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rigibody.velocity = Vector3.zero;
            downTorret = true;
            upTorret = false;
        }
    }

    private void UpTorret()
    {
        rigibody.AddForce((forceToApply * Vector3.up) * Time.deltaTime, ForceMode.Acceleration);
    }

    private void DownTorret()
    {
        rigibody.AddForce((forceToApply * Vector3.down) * Time.deltaTime, ForceMode.Acceleration);
    }

    public void RotateTorret()
    {
        Quaternion originalRotation = transform.rotation;
        Vector3 lookDirection = player.transform.position - transform.position;
        lookDirection.y = 0;

        Quaternion destinationRotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        Quaternion rotationToDestination = destinationRotation * Quaternion.Inverse(originalRotation);

        Vector3 velocity = rotationToDestination.eulerAngles;
        velocity.x = NormalizedAngle(velocity.x);
        velocity.y = NormalizedAngle(velocity.y);
        velocity.z = NormalizedAngle(velocity.z);

        velocity = 0.1f * Mathf.Deg2Rad * velocity - rigibody.angularVelocity * 0.1f;
        //transform.rotation = rotationToDestination * originalRotation;

        rigibody.AddTorque(velocity, ForceMode.VelocityChange);

        //Vector3 direction = transform.forward;

        //// direction to look at (not normalized)
        //Vector3 lookDirection = (player.transform.position - transform.position);
        ////direction.y = 0.0f;

        ////float angleToRotate = Mathf.Deg2Rad * Vector3.SignedAngle(direction, lookDirection, transform.up);

        //// Vector3 velocityVariation  = 0.01f * angleToRotate * transform.up - rigibody.angularVelocity;

        //Vector3 velocityVariation = (Quaternion.FromToRotation(direction, lookDirection).eulerAngles * Mathf.Deg2Rad); // - rigibody.angularVelocity
        //transform.rotation = Quaternion.FromToRotation(direction, lookDirection) * transform.rotation;

        //// Clamp velocity using unit angular acceleration
        ///// velocityVariation.y = accelerationAngular * Time.deltaTime;

        //// Rotate in local coordinates
        //// rigibody.AddTorque(velocityVariation, ForceMode.VelocityChange);
    }

    public float NormalizedAngle(float angle)
    {
        while(angle > 180)
        {
            angle -= 360; 
        }

        return angle;
    }

    public static void ClampModule(Vector3 origin, float moduleLimit)
    {
        float sqrModule = origin.sqrMagnitude;

        if (sqrModule > moduleLimit * moduleLimit)
        {
            origin *= moduleLimit / Mathf.Sqrt(sqrModule);
        }
    }

}
