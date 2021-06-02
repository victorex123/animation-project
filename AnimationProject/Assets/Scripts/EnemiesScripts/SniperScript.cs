using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperScript : MonoBehaviour
{
    //Public variables

    public float maxHP = 15;
    public GameObject explosionVFX;
    public GameObject laserBullet;
    public Transform bulletPos1;
    public Transform bulletPos2;
    public float cooldown = 4;

    //Private variables

    GameObject target;
    private float actualHP;
    private bool activated = true;

    private float dt;
    private float yRot;

    private float shootTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        actualHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;

        if (activated)
        {
            if (target != null)
            {
                transform.LookAt(target.transform.position);
                shootTimer += dt;
                if (shootTimer >= cooldown)
                {
                    GameObject newLaser1 = Instantiate(laserBullet, bulletPos1.position, Quaternion.identity);
                    newLaser1.GetComponent<LaserBulletCode>().SetTarget(target.transform);
                    GameObject newLaser2 = Instantiate(laserBullet, bulletPos2.position, Quaternion.identity);
                    newLaser2.GetComponent<LaserBulletCode>().SetTarget(target.transform);
                    shootTimer = 0;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            actualHP -= 1;
            if (actualHP <= 0 && activated)
            {
                activated = false;
                transform.rotation = Quaternion.Euler(45, transform.rotation.eulerAngles.y, 0);
                GameObject explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
                Destroy(explosion,4);
            }
        }
    }
}
