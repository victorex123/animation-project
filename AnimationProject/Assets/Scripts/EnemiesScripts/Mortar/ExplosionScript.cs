using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    // Public attributes

    public float radius;
    public float power;

    public GameObject explosionVFX;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Explosion generada");
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        GameObject explosion = Instantiate(explosionVFX, explosionPos, Quaternion.identity);
        explosion.GetComponent<DestroyParticle>().setTimer(2);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0f);
                float totalDamage = 150 - (150 * Vector3.Distance(explosionPos, hit.transform.position) / radius);

                if (hit.CompareTag("Player"))
                {
                    hit.GetComponent<PlayerManager>().ReceiveDamage(totalDamage, 0);
                }
                if (hit.CompareTag("Enemy"))
                {
                    hit.GetComponent<EnemyHeal>().TakeDamage(totalDamage*2);
                }

            }
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetValues(float radius, float power)
    {
        this.radius = radius;
        this.power = power;
    }
}
