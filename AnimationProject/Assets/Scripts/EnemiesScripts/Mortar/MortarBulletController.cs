using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBulletController : MonoBehaviour
{
    // Public attributes

    public float flyingTime;

    // Private attributes

    private float gravity = 9.8f;

    private float initialAngle;

    private float initialSpeed;

    private Transform target;

    private float dt;


    // Start is called before the first frame update
    void Start()
    {
        initialSpeed = Vector3.Distance(transform.position, target.position) / flyingTime * -1;

        var lookPos = transform.position - target.position;
        //lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;

        if (Vector3.Distance(transform.position, target.transform.position) > initialSpeed)
        {
            transform.position += transform.forward * initialSpeed * dt;
        }
    }
}
