using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyConection : MonoBehaviour
{
    //Private
    float hp = 4;
    [SerializeField]
    GameObject child;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hp -= 1;
            if (hp <= 0)
            {
                Destroy(collision.gameObject);
                child.GetComponent<Rigidbody>().isKinematic = false;
                child.GetComponent<Rigidbody>().useGravity = true;
                child.transform.parent = null;
                Destroy(this.transform.parent.gameObject);
            }
        }
    }
}
