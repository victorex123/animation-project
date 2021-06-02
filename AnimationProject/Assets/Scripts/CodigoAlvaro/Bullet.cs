using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int mode;
    private bool explosive = false;
    public int damage;
    [SerializeField]
    private GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5);
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
            damage = 20;
        }
        if (mode == 1)
        {
            damage = 25;
        }
        if (mode == 2)
        {
            damage = 100;
            explosive = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (explosive)
        {
            GameObject.Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
        }

            Destroy(gameObject);

    }
}
