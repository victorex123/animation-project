using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirConductor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float length;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private float force;
    private Vector3 changePoint;
    [SerializeField]
    private float Scalemultiplier;

    void Start()
    {
       direction = force * (direction.normalized);
        config();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void config()
    {
        Vector3 aux = gameObject.GetComponent<CapsuleCollider>().center;
        aux.y = 0;
        gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
        gameObject.GetComponent<CapsuleCollider>().height = length * 100* Scalemultiplier;
        gameObject.GetComponent<CapsuleCollider>().center = aux;
        gameObject.GetComponent<CapsuleCollider>().center+= new Vector3(0, (50 * (length - 1))* Scalemultiplier, 0);
        gameObject.GetComponent<CapsuleCollider>().radius = 0.5f;
        changePoint = aux;
        changePoint += new Vector3(0,length-0.5f,0);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().AddForce(direction * 0.01f, ForceMode.Acceleration);
    }*/

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Force);
    }

}
