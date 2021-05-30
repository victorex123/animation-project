using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private Transform firstPoint;
    [SerializeField]
    private Transform secondPoint;
    private bool next = true;
    private Vector3 dir;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float marginError;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = firstPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (next)
        {
            dir = Vector3.Normalize(secondPoint.position - firstPoint.position) * speed * Time.deltaTime;
        }
        else
        {
            dir = Vector3.Normalize(firstPoint.position - secondPoint.position) * speed * Time.deltaTime;
        }
        gameObject.transform.position += dir;
        if(Vector3.Distance(firstPoint.position, gameObject.transform.position) < marginError && !next || Vector3.Distance(secondPoint.position, gameObject.transform.position) < marginError && next)
        {
            if (next)
            {
                next = false;
            } 
            else
            {
                next = true;
            }
        }
    }
}
