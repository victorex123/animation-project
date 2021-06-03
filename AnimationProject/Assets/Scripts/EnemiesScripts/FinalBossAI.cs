using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossAI : MonoBehaviour
{
    // Start is called before the first frame update
    // Public
    public GameObject player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
    }
}
