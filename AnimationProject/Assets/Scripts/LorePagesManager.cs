using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LorePagesManager : MonoBehaviour
{
    public LorePage lorePages;

    public GameObject controlPanel;
    public GameObject enemiesPanel;
    public GameObject objectivePanel;
    public GameObject buttonPanel;
    public GameObject principalLoreCanvas;

    public Text displayEnemyText;
    public Image displayEnemyImage;
    public Text displayEnemyTitle;

    public Text displayControlText;
    public Image displayControlImage;
    public Text displayControlTitle;

    private int i=0;    

    private GameObject actualCanvas;
    private string[] actualCharacteristicsList;
    private Sprite[] actualSpriteList;
    private string[] actualTitleList;
    // Start is called before the first frame update
    void Start()
    {
        actualCharacteristicsList = new string[0];
        actualSpriteList = new Sprite[0];
    }

    public void BackButtonsPanel()
    {
        actualCanvas.SetActive(false);
        buttonPanel.SetActive(true);
    }

    public void CloseLorePages()
    {
        principalLoreCanvas.SetActive(false);
    }

    public void GoToRightPage()
    {
        i++;
        if (i>=actualCharacteristicsList.Length)
        {
            i = actualCharacteristicsList.Length-1;
            return;
        }
        else
        {
            if (actualCanvas == enemiesPanel)
            {
                displayEnemyText.text = actualCharacteristicsList[i];
                displayEnemyImage.sprite = actualSpriteList[i];
            }
            else if (actualCanvas == controlPanel)
            {
                displayControlText.text = lorePages.controlCharacteristicsList[i];
                displayControlImage.sprite = lorePages.controlImagesList[i];
            }
        }
        

    }

    public void GoToLeftPage()
    {
        i--;
        if(0>i)
        {
            i = 0;
            return;
        }
        else
        {
            if(actualCanvas == enemiesPanel)
            {
                displayEnemyText.text = actualCharacteristicsList[i];
                displayEnemyImage.sprite = actualSpriteList[i];
            }
            else if (actualCanvas == controlPanel)
            {
                displayControlText.text = lorePages.controlCharacteristicsList[i];
                displayControlImage.sprite = lorePages.controlImagesList[i];
            }
            
        }
        
    }

    public void EnemiesPage()
    {
        buttonPanel.SetActive(false);
        enemiesPanel.SetActive(true);
        actualCanvas = enemiesPanel;

        i = 0;
        CleanArrays();
        actualCharacteristicsList = lorePages.enemyCharacteristicList;
        actualSpriteList = lorePages.enemyImagesList;
        actualTitleList = lorePages.enemyNamesList;

        displayEnemyText.text = lorePages.enemyCharacteristicList[i];
        displayEnemyImage.sprite = lorePages.enemyImagesList[i];

    }

    public void ControlPage()
    {
        buttonPanel.SetActive(false);
        controlPanel.SetActive(true);
        actualCanvas = controlPanel;

        i = 0;
        CleanArrays();
        actualCharacteristicsList = lorePages.controlCharacteristicsList;
        actualSpriteList = lorePages.controlImagesList;
        actualTitleList = lorePages.controlNamesList;

        displayControlText.text = lorePages.controlCharacteristicsList[i];
        displayControlImage.sprite = lorePages.controlImagesList[i];
    }

    public void ObjetivePage()
    {
        buttonPanel.SetActive(false);
        objectivePanel.SetActive(true);
        actualCanvas = objectivePanel;
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("Player")) && Input.GetKeyDown(KeyCode.Q))
        {
            principalLoreCanvas.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            principalLoreCanvas.SetActive(false);
        }
    }

    public void CleanArrays()
    {
        Array.Clear(actualCharacteristicsList, 0, actualCharacteristicsList.Length);
        Array.Clear(actualSpriteList, 0, actualSpriteList.Length);
        //Array.Clear(actualTitleList, 0, actualTitleList.Length);



    }

}
