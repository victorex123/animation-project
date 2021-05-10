using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public float speed = 90.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.AngleAxis(speed * Time.deltaTime, transform.up) * transform.rotation;
        transform.position = Vector3.up * 5.0f * Mathf.Sin( (Time.time * 3.0f));
    }
}
