using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBulletController : MonoBehaviour
{
    // Public attributes

    public Transform target;
    public float flyingTime;
    public GameObject explosionObject;

    public float radius;
    public float power;

    // Private attributes

    private float gravity = 9.8f;
    private float initialSpeedX, initialSpeedY;
    private float dt;
    private float timeOnAir;

    // Start is called before the first frame update
    void Start()
    {
        initialSpeedX = Vector3.Distance(transform.position, target.position) / flyingTime * -1;
        initialSpeedY = (gravity * flyingTime) / 2;

        var lookPos = transform.position - target.position;
        //lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);

        timeOnAir = 0;
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;

        timeOnAir += dt;

        transform.position += transform.forward * initialSpeedX * dt;
        transform.position += transform.up * (initialSpeedY - gravity * timeOnAir) * dt;

        if (timeOnAir > flyingTime+0.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Cagaste");
            other.GetComponent<PlayerManager>().ReceiveDamage(100, 0);
            //Crear explosión
            CreateExplosion();
        }

        if (other.CompareTag("Terrain"))
        {
            Debug.Log("Impacto contra el suelo");
            //Crear explosión
            CreateExplosion();
        }

    }
    private void CreateExplosion()
    {
        GameObject explosion = Instantiate(explosionObject, transform.position, Quaternion.identity);
        explosion.GetComponent<ExplosionScript>().SetValues(radius, power);
        Destroy(this.gameObject);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
