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
    public Text displayText;
    public Image displayImage;
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
            print(i);
            return;
        }
        else
        {
            displayText.text = actualCharacteristicsList[i];
            displayImage.sprite = actualSpriteList[i];
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
            displayText.text = actualCharacteristicsList[i];
            displayImage.sprite = actualSpriteList[i];
        }
        
    }

    public void EnemiesPage()
    {
        buttonPanel.SetActive(false);
        enemiesPanel.SetActive(true);
        actualCanvas = enemiesPanel;

        //CleanArrays();
        actualCharacteristicsList = lorePages.enemyCharacteristicList;
        actualSpriteList = lorePages.enemyImagesList;
        actualTitleList = lorePages.enemyNamesList;

        displayText.text = lorePages.enemyCharacteristicList[i];
        displayImage.sprite = lorePages.enemyImagesList[i];

    }

    public void ControlPage()
    {
        buttonPanel.SetActive(false);
        controlPanel.SetActive(true);
        actualCanvas = controlPanel;
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
        for(int j=0;j<actualCharacteristicsList.Length;j++)
        {
            actualCharacteristicsList[j] = "";
        }

        for(int j = 0; j < actualSpriteList.Length; j++)
        {
            actualSpriteList[j] = null;
        }

        for (int j = 0; j < actualTitleList.Length; j++)
        {
            actualTitleList[j] = "";
        }
    }

}
