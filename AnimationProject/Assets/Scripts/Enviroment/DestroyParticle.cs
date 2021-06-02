using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    public float timer = 4;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setTimer(float time)
    {
        Destroy(this.gameObject, time);
    }
}
