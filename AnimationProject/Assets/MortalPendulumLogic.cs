using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortalPendulumLogic : MonoBehaviour
{
    // Start is called before the first frame update
    //Private
    float minAngle = -90;
    float maxAngle = 90;
    float actualAngle;

    float speed;
    float aceleration = 1;

    float dt;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;

        speed += aceleration * dt;
        actualAngle = transform.rotation.z + speed * dt;

        transform.rotation = Quaternion.AngleAxis(actualAngle, Vector3.forward);

        Debug.Log(actualAngle);
    }
}
