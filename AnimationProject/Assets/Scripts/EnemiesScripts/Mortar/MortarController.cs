using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarController : MonoBehaviour
{
    // Public attributes

    public Transform target;

    public GameObject bulletType;

    public float timeBetweenShoots = 10;

    public Transform spawnBulletPos;

    public GameObject turretMan;

    // Private attributes

    private float dt;
    private float timerBetwennShoots = 0;
    private bool active = true;
    private float maxArtilleroDistance = 8;



    // Start is called before the first frame update
    void Start()
    {
        timerBetwennShoots = 0;
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;

        if (active || turretMan != null)
        {
            BeingControlled();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ( target != null && other.gameObject.CompareTag("Player"))
        {
            target = null;
        }
    }
    private void Shoot()
    {
        timerBetwennShoots += dt;
        if (timerBetwennShoots > timeBetweenShoots && target != null)
        {
            timerBetwennShoots = 0;
            GameObject bullet = Instantiate(bulletType, spawnBulletPos.position, Quaternion.identity);
            bullet.GetComponent<MortarBulletController>().SetTarget(target);
        }
    }
    private void BeingControlled()
    {
        if (Vector3.Distance(transform.position, turretMan.transform.position) <= maxArtilleroDistance)
        {
            Shoot();
        }
        if (turretMan.GetComponent<EnemyHeal>().currentHealth <= 0)
        {
            active = false;
        }

    }
}
