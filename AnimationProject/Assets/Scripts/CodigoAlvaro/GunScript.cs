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
    private GameObject bright;
    private float bulletForwardForce;
    [SerializeField]
    private int mode = 0;
    private float shootCooldown;
    private int ammo = 0;
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
        if(ammo > 0)
        {
            GameObject aux;
            aux = GameObject.Instantiate(bullet, bulletPoint.transform.position, Quaternion.identity);
            GameObject.Instantiate(bright, bulletPoint.transform.position, Quaternion.identity);
            aux.transform.up = transform.forward;
            aux.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForwardForce, ForceMode.Impulse);
            ammo -= 1;
        }
    }

    public void settings()
    {
        if(mode == 0)
        {
            shootCooldown = 0.75f;
            bulletForwardForce = 50f;
            ammo = 18;
        }
        if(mode == 1)
        {
            shootCooldown = 0.2f;
            bulletForwardForce = 75f;
            ammo = 32;
        }
        if(mode == 2)
        {
            shootCooldown = 3f;
            bulletForwardForce = 75f;
            ammo = 3;
        }
    }

    public float GetShootCooldown()
    {
        return shootCooldown;
    }

    public int GetMode()
    {
        return mode;
    }

    public void setAmmo(int munition)
    {
        ammo += munition;
    }

    public int GetAmmo()
    {
        return ammo;
    }
}
