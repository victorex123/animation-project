using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarController : MonoBehaviour
{
    // Public attributes

    public Transform target;

    public GameObject bulletType;

    // Private attributes

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
}
