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
        Vector3 direction = transform.forward;

        // direction to look at (not normalized)
        Vector3 lookDirection = (player.transform.position - transform.position);
        direction.y = 0.0f;

        float angleToRotate = Vector3.SignedAngle(direction, lookDirection, Vector3.up);

        Vector3 velocityVariation = angleToRotate * Vector3.up - rigibody.angularVelocity;

        // Clamp velocity using unit angular acceleration
       /// velocityVariation.y = accelerationAngular * Time.deltaTime;

        // Rotate in local coordinates
        rigibody.AddRelativeTorque(velocityVariation, ForceMode.VelocityChange);
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
