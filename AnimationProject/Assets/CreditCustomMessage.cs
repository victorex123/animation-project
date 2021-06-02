using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditCustomMessage : MonoBehaviour
{
    // Variables

    public Text customText;

    void Start()
    {
        customText.text = "Juego completado en dificultad: ####\n Número de muertes: " + SingeltonData.instance.deads +
            "\n\n" + customText.text;
    }
}
