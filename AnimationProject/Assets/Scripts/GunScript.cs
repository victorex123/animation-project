using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPoint;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float bulletForwardForce = 1;
    private float mode = 0;
    [SerializeField]
    private float shootCooldown;
    // Start is called before the first frame update
    void Start()
    {
        settings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        GameObject aux;
        aux = GameObject.Instantiate(bullet, bulletPoint.transform.position,Quaternion.identity);
        aux.transform.up = transform.forward;
        aux.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForwardForce, ForceMode.Impulse);
    }

    public void settings()
    {
        if(mode == 0)
        {
            shootCooldown = 0.5f;
            bulletForwardForce = 75f;
        }
    }

    public float GetShootCooldown()
    {
        return shootCooldown;
    }
}
