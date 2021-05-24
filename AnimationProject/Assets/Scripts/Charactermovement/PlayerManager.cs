using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    //Public attributes
    public float maxHealth;
    public Slider healthBar;

    //Private attributes
    private float actualHealth;
    private float dt;
    private Color startColor;
    private Color finalColor;


    // Start is called before the first frame update
    void Start()
    {
        actualHealth = maxHealth;
        healthBar.value = actualHealth / maxHealth;
        startColor = new Color(0, 255, 63);
        finalColor = new Color(255, 0, 0);
        healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = startColor;
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;

        if (Input.GetKey(KeyCode.K))
        {
            ChangeHealthBar(20f * dt);
        }

        if (Input.GetKey(KeyCode.H))
        {
            ChangeHealthBar(-20f * dt);
        }
    }

    private void ChangeHealthBar(float newValue)
    {
        actualHealth = actualHealth - newValue;

        if (actualHealth < 0) actualHealth = 0;

        // Poner cambio de color
        float percentaje = actualHealth / maxHealth;
        //Vector3 colorVector = new Vector3(255, 255, 255);
        //Color actualColor = New
        //healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = actualColor;
        healthBar.value = percentaje;
    }
}
