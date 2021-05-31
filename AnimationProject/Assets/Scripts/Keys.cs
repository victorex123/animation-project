using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Keys : MonoBehaviour
{
    public List<Transform> waypoints;
    private int randomNumber;
    public GameObject handPlayer;

    // Start is called before the first frame update
    void Start()
    {
        randomNumber = Random.Range(0, waypoints.Count);
        transform.position = waypoints[randomNumber].position;
        handPlayer.SetActive(false);
        //print(key.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //key.transform.Rotate(0.0f, 90.0f, 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //print("hola");
            handPlayer.SetActive(true);
            transform.position = handPlayer.transform.position;
            transform.SetParent(handPlayer.transform);
        }

        else if (other.CompareTag("Door"))
        {
            handPlayer.SetActive(false);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

}
