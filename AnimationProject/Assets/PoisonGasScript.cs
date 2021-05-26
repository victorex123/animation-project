using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGasScript : MonoBehaviour
{
    //Public

    public float damage;


    //Private

    private float dt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerManager>().ReceiveDamage(damage * dt, 1);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerManager>().CleanPoison();
        }
    }
}
