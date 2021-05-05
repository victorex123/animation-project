using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretEnemy : MonoBehaviour
{
    private Rigidbody rigibody;
    public float maxTimeToUp = 5.0f;
    public float time = 0;
    public float maxTimeToDown = 0.0f;
    public bool upTorret = false;
    public bool downTorret = false;
    public float forceToApply = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigibody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(upTorret)
        {
            if (time >= maxTimeToUp)
            {
                upTorret = false;
                rigibody.velocity = Vector3.zero;
            }
            else 
            {
                time += Time.deltaTime;
                UpTorret();
            }
        }

        if(downTorret)
        {
            if (time <= maxTimeToDown)
            {
                downTorret = false;
                rigibody.velocity = Vector3.zero;
            }
            else
            {
                time -= Time.deltaTime;
                DownTorret();
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rigibody.velocity = Vector3.zero;
            upTorret = true;
            downTorret = false; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rigibody.velocity = Vector3.zero;
            downTorret = true;
            upTorret = false;
        }
    }

    private void UpTorret()
    {
        rigibody.AddForce((forceToApply * Vector3.up) * Time.deltaTime, ForceMode.Acceleration);
    }

    private void DownTorret()
    {
        rigibody.AddForce((forceToApply * Vector3.down) * Time.deltaTime, ForceMode.Acceleration);
    }
}
