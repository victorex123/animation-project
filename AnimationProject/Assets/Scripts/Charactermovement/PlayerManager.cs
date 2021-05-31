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

    public Image fadeInFullScreen;

    //Private attributes
    private float actualHealth;
    private float dt;
    private Color startColor;
    private Color finalColor;

    private float dyingAnimationTime = 4;
    private float dyingAnimationTimer = 0;
    private float fadeInSpeed = 0.25f;

    private float fallDamage = 0;

    private bool cinematicMode = false;
    private int fading = 0;
    private int specialInitialState;
    private float fadingCinemaTimer = 0;
    private float fadingCinemaTime = 0;

    private string nextScene;
    private bool teleporting = false;
    private float tpTimer = 0;
    private float tpTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        LoadDataSingelton();

        //actualHealth = maxHealth;
        healthBar.value = actualHealth / maxHealth;
        startColor = new Color(255, 0, 0);
        finalColor = new Color(255, 0, 0);
        healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = startColor;
    }

    // Update is called once per frame
    void Update()
    {
        //print("SCPIRT" + actualHealth);

        dt = Time.deltaTime;

        if (actualHealth <= 0)
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

        FallDamage();

        ManageFading();

        ManageEvents();
    }

    public void OnDestroy()
    {
        if (actualHealth > 0)
        {
            SaveDataSingelton();
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
            actualHealth -= amount;
            healthBar.value = actualHealth / maxHealth;
            if (type == 1)
            {
                healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(0.15f, 0.5f, 0.15f);
            }
        }
        if (actualHealth <= 0)
        {
            playerController.KillPlayer();
            GetComponent<Rigidbody>().freezeRotation = false;
            
        }

        //print(actualHealth);
    }
    public void CleanPoison()
    {
        healthBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = startColor;
    }

    private void LoadDataSingelton()
    {
        actualHealth = SingeltonData.instance.actualHealth;
        specialInitialState = SingeltonData.instance.specialInitialState;
        fadeInFullScreen.color = SingeltonData.instance.fadeColor;

        ManageInitialState();
    }

    private void SaveDataSingelton()
    {
        SingeltonData.instance.actualHealth = actualHealth;
        SingeltonData.instance.specialInitialState = specialInitialState;
        SingeltonData.instance.fadeColor = fadeInFullScreen.color;

    }

    private void FallDamage()
    {
        float actualYSpeed = GetComponent<Rigidbody>().velocity.y;
        if (actualYSpeed < -10)
        {
            fallDamage = -actualYSpeed;
        }

        if (actualYSpeed < -25 && transform.position.y < 0)
        {
            ReceiveDamage(99999999, 0);
        }

        if (actualYSpeed == 0 && fallDamage != 0)
        {
            ReceiveDamage(fallDamage, 0);
            fallDamage = 0;
        }

    }

    private void ManageInitialState()
    {
        if (specialInitialState == 1)
        {
            cinematicMode = true;
            playerController.SetCinematicMode(cinematicMode);
            fadingCinemaTime = 3;
            fading = 2;
            specialInitialState = 0;
        }
    }
    public void TeleportScenePlayer(string newScene, float timeToWarp, Color fadeColor)
    {
        cinematicMode = true;
        playerController.SetCinematicMode(cinematicMode);
        fadeInFullScreen.color = fadeColor;
        specialInitialState = 1;
        fadingCinemaTime = timeToWarp;
        fading = 1;
        tpTime = timeToWarp;
        teleporting = true;
        nextScene = newScene;
    }
    public void ManageFading()
    {
        if (fading == 0) return;

        Color newColor;
        fadingCinemaTimer += dt;

        if (fading == 1) //Fade in
        {
            newColor = new Color(fadeInFullScreen.color.r, fadeInFullScreen.color.g, fadeInFullScreen.color.b, 
                fadingCinemaTimer/fadingCinemaTime*1f);
            fadeInFullScreen.color = newColor;
        }

        if (fading == 2) //Fade out
        {
            newColor = new Color(fadeInFullScreen.color.r, fadeInFullScreen.color.g, fadeInFullScreen.color.b,
                1 - fadingCinemaTimer / fadingCinemaTime*1f);
            fadeInFullScreen.color = newColor;
        }

        if (fadingCinemaTimer > fadingCinemaTime)
        {
            if (fading == 2)
            {
                cinematicMode = false;
                playerController.SetCinematicMode(cinematicMode);
            }
            fading = 0;
            fadingCinemaTimer = 0;
            fadingCinemaTime = 0;
        }
    }
    public void ManageEvents()
    {
        if (teleporting)
        {
            tpTimer += dt;

            if (tpTimer > tpTime)
            {
                teleporting = false;
                tpTimer = 0;
                SceneManager.LoadScene(nextScene);
            }         
        }
    }
}
