using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditCustomMessage : MonoBehaviour
{
    // Variables

    public Text customText;

    float creditsDuration = 20;
    float timer = 0;

    void Start()
    {
        string difficultText = "normal";
        switch (SingeltonData.instance.difficult)
        {
            case 0:
                difficultText = "easy";
                break;
            case 1:
                difficultText = "normal";
                break;
            case 2:
                difficultText = "hard";
                break;
        }
        customText.text = "Game completed on "+ difficultText +"\n Number of deads: " + SingeltonData.instance.deads +
            "\n\n" + customText.text;
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        timer += dt;

        if (timer >= creditsDuration)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            SceneManager.LoadScene("MainMenu");
        }

    }
}
