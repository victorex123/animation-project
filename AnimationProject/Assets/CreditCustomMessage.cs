using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditCustomMessage : MonoBehaviour
{
    // Variables

    public Text customText;

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

        Destroy(this.gameObject,20);
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
