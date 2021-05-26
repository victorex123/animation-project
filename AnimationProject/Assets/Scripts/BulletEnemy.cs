using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float damage;
    private PlayerManager player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerManager>();
            player.ReceiveDamage(damage, 0);

            Destroy(this.gameObject);
        }

        else if(other.CompareTag("Terrain"))
        {
             Destroy(this.gameObject);
        }

    }
}
