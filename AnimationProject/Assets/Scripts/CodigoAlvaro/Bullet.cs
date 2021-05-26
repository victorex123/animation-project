using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int mode;
    private bool explosive = false;
    private float damage;
    [SerializeField]
    private GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        settings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetDamage()
    {
        return damage;
    }

    public void settings()
    {
        if (mode == 0)
        {
            damage = 10;
        }
        if (mode == 1)
        {
            damage = 5;
        }
        if (mode == 2)
        {
            damage = 20;
            explosive = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(explosive)
        {
            GameObject.Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
