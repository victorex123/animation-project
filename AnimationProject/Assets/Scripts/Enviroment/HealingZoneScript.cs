using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZoneScript : MonoBehaviour
{
    // Start is called before the first frame update
    //Public variables
    public float healAmount = 1;

    //Private variables
    private float dt;

    private void Update()
    {
        dt = Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) other.gameObject.GetComponent<PlayerManager>().ReceiveDamage(-healAmount * dt, 0);
    }
}
