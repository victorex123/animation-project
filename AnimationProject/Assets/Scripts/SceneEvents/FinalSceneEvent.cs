using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSceneEvent : MonoBehaviour
{
    // Start is called before the first frame update
    // Public

    public GameObject[] enemies;

    public GameObject laserWall;

    public GameObject [] defensiveTurrets;

    public GameObject forceShield;

    public GameObject player;


    // Private
    int actualEvent = 0;
    int allKilled = 0;
    int allDesactivated = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (actualEvent)
        {
            case 0:
                allKilled = 0;
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].GetComponent<EnemyHeal>().currentHealth <= 0) allKilled++;
                }
                if (allKilled == enemies.Length) actualEvent++;
                break;
            case 1:
                laserWall.SetActive(false);
                actualEvent++;
                break;
            case 2:
                allDesactivated = 0;
                for (int i = 0; i < defensiveTurrets.Length; i++)
                {
                    if (!defensiveTurrets[i].GetComponent<SniperScript>().GetActivated()) allDesactivated++;
                }
                if (allDesactivated == defensiveTurrets.Length) actualEvent++;
                break;
            case 3:
                forceShield.SetActive(false);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && actualEvent == 3)
        {
            Debug.Log("Juego completado");
            actualEvent++;
            Vector3 colorValues = new Vector3(0 / 255f, 0 / 255f, 0 / 255f);
            Color fadeColor = new Color(colorValues.x, colorValues.y, colorValues.z, 0f);
            other.GetComponent<PlayerManager>().TeleportScenePlayer("Credits", 3, fadeColor);
        }
    }
}
