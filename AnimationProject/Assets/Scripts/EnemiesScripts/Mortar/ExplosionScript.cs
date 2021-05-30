using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    // Public attributes

    public float radius;
    public float power;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Explosion generada");
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0f);
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
