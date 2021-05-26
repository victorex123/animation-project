using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    //Public attributes
    public float maxHealth;
    public Slider healthBar;

    public PlayerController playerController;

    public Image fadeIn;
    public Text deathMessage;

    //Private attributes
    private float actualHealth;
    private float dt;
    private Color startColor;
    private Color finalColor;

    private float dyingAnimationTime = 4;
    private float dyingAnimationTimer = 0;
    private float fadeInSpeed = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        actualHealth = maxHealth;
        healthBar.value = actualHealth / maxHealth;
        startColor = new Color(255, 0, 0);
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

        if (actualHealth < 0)
        {
            dyingAnimationTimer += dt;
            if (fadeIn.color.a + fadeInSpeed*dt <= 0.75f)
            {
                fadeIn.color = new Color(0, 0, 0, fadeIn.color.a + fadeInSpeed * dt);
                deathMessage.color= new Color(deathMessage.color.r, deathMessage.color.g, deathMessage.color.b, deathMessage.color.a + fadeInSpeed * dt);
            }

            if (dyingAnimationTimer >= dyingAnimationTime)
            {
                Debug.Log("Time OUT");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void ChangeHealthBar(float newValue)
    {
        actualHealth -= newValue;

        if (actualHealth < 0) actualHealth = 0;

        // Poner cambio de color
        float percentaje = actualHealth / maxHealth;
        //Vector3 colorVector = new Vector3(255, 255, 255);
        //Color actualColor = New
        //healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = actualColor;
        healthBar.value = percentaje;
    }

    public void ReceiveDamage(float amount, int type)
    {
        if (actualHealth > 0)
        {
            actualHealth -= amount * dt;
            healthBar.value = actualHealth / maxHealth;
            if (type == 1)
            {
                healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(0.15f, 0.5f, 0.15f);
            }
        }
        else if (actualHealth <= 0)
        {
            playerController.killPlayer();
        }

        print(actualHealth);
    }
    public void CleanPoison()
    {
        healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = startColor;
    }

}
