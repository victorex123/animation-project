using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.XR;

public class Keys : MonoBehaviour
{
    public List<Transform> waypoints;
    private int randomNumber;
    public GameObject handPlayer;
    private bool handOn;

    // Start is called before the first frame update
    void Start()
    {
        randomNumber = Random.Range(0, waypoints.Count);
        print(waypoints.Count);
       
        transform.position = waypoints[randomNumber].position;
        handPlayer.SetActive(false);
        //print(key.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(!handOn)
        {
            transform.Rotate(0.0f, 1.0f, 0.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //print("hola");
            handPlayer.SetActive(true);
            putKeyHand();
            handOn = true;
            
        }

        else if (other.CompareTag("Door"))
        {
            handPlayer.SetActive(false);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void putKeyHand()
    {
        transform.position = handPlayer.transform.position;
        transform.rotation = handPlayer.transform.rotation;
        transform.localScale = new Vector3(0.5f, 1.0f, 0.5f);
        transform.SetParent(handPlayer.transform);
    }

}
