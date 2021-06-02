using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortalPendulumLogic : MonoBehaviour
{
    // Start is called before the first frame update
    //Private
    float minAngle = -90;
    float maxAngle = 90;
    float actualAngle = -90;

    float speed = 60;
    float direction = 1;

    float dt;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;

        actualAngle += speed * direction * dt;

        if (actualAngle + speed * direction * dt <= minAngle || actualAngle + speed * direction * dt >= maxAngle)
        {
            direction *= -1;
        }

        transform.rotation = Quaternion.Euler(0,0,actualAngle);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerManager>().ReceiveDamage(9999, 0);
        }
    }
}
