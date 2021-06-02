using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretEnemy : MonoBehaviour
{
    private Rigidbody rigibody;
    public float maxTimeToUp = 5.0f;
    public float timeToWaitToMove = 0;
    public float maxTimeToDown = 0.0f;
    public bool upTorret = false;
    public bool downTorret = false;
    public float forceToApplyToUpp = 5.0f;
    private GameObject player;
    public float accelerationAngular = 100.0f;
    public float speedShoot;

    private bool detectPlayer = false;
    private float timeToShoot;
    public float maxTimeShoot = 6.0f;
    private float timeWasteToShoot;
    public GameObject bullet;
    public GameObject bulletPos;
    public GameObject healtBar;

    public EnemyHeal health;
    public float distanceToFollowPlayer = 0.0f;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        rigibody = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        health = GetComponent<EnemyHeal>();
        timeToShoot = UnityEngine.Random.Range(1.0f, maxTimeShoot);
        timeWasteToShoot = timeToShoot;
        healtBar.SetActive(false);

        if (SingeltonData.instance.difficult == 0)
        {
            speedShoot = 10;
        }
        else if (SingeltonData.instance.difficult == 1)
        {
            speedShoot = 20;
        }
        else if (SingeltonData.instance.difficult == 2)
        {
            speedShoot = 50;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health.currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }

        distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance<=distanceToFollowPlayer)
        {
            rigibody.velocity = Vector3.zero;
            upTorret = true;
            downTorret = false;
            detectPlayer = true;
            healtBar.SetActive(true);
        }
        else
        {
            rigibody.velocity = Vector3.zero;
            downTorret = true;
            upTorret = false;
            detectPlayer = false;
            healtBar.SetActive(false);
        }

        RotateTorret();
        CheckState();
    }



    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
            

    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
            
    //    }
    //}

    public void CheckState()
    {
        if (upTorret)
        {
            if (timeToWaitToMove >= maxTimeToUp)
            {
                upTorret = false;
                rigibody.velocity = Vector3.zero;

            }
            else
            {
                timeToWaitToMove += Time.deltaTime;
                UpTorret();
            }
        }

        if (downTorret)
        {
            if (timeToWaitToMove <= maxTimeToDown)
            {
                downTorret = false;
                rigibody.velocity = Vector3.zero;
            }
            else
            {
                timeToWaitToMove -= Time.deltaTime;
                DownTorret();
            }
        }

        if (detectPlayer)
        {
            if (timeWasteToShoot >= maxTimeShoot)
            {
                Shoot();
            }
            else
            {
                timeWasteToShoot += Time.deltaTime;
            }
        }
    }
    private void UpTorret()
    {
        rigibody.AddForce((forceToApplyToUpp * Vector3.up) * Time.deltaTime, ForceMode.Acceleration);
    }

    private void DownTorret()
    {
        rigibody.AddForce((forceToApplyToUpp * Vector3.down) * Time.deltaTime, ForceMode.Acceleration);
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

    public void Shoot()
    {
        GameObject aux;
        aux = Instantiate(bullet, bulletPos.transform.position, Quaternion.identity);

        aux.transform.LookAt(player.transform.position);
        aux.GetComponent<Rigidbody>().AddForce(aux.transform.forward * speedShoot, ForceMode.Impulse);

        timeWasteToShoot = timeToShoot;
    }

}
