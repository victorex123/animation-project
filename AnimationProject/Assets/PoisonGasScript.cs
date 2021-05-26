using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGasScript : MonoBehaviour
{
    //Public

    public float damage;


    //Private



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("In range with poison gas");
            other.GetComponent<PlayerManager>().ReceiveDamage(damage, 1);
        }
    }
}
