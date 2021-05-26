using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer;
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= 0.05f)
        {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
    }
}
