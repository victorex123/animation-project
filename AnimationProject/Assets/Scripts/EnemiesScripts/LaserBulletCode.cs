using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBulletCode : MonoBehaviour
{
    // Start is called before the first frame update
    // Public

    public Transform target;
    public float laserBulletSpeed = 20;
    public float force = 25;
    public float laserDamage = 15;

    // Private

    private float dt;
    private Vector3 direction;

    void Start()
    {
        direction = (target.transform.position - transform.position).normalized;
        transform.LookAt(target);
        Destroy(this.gameObject,15);
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        transform.position += transform.forward * laserBulletSpeed * dt;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerManager>().ReceiveDamage(laserDamage, 0);
            other.gameObject.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.VelocityChange);
            Destroy(this.gameObject);
        }
    }
    public void SetTarget(Transform newVector)
    {
        target = newVector;
    }
}
