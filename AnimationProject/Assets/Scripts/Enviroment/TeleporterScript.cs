using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterScript : MonoBehaviour
{
    public string targetScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 colorValues = new Vector3(238 / 255f, 130 / 255f, 238 / 255f);
            Color fadeColor = new Color(colorValues.x, colorValues.y, colorValues.z, 0f);
            other.GetComponent<PlayerManager>().TeleportScenePlayer(targetScene, 3, fadeColor);
        }
    }
}
