using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LorePagesManager : MonoBehaviour
{
    public LorePage lorePages;

    Queue<string> enemySentences;
    Queue<Image> enemyImages;

    public GameObject controlPanel;
    public GameObject enemiesPanel;
    public GameObject objectivePanel;
    public GameObject buttonPanel;
    public GameObject principalLoreCanvas;
    public Text displayText;
    public Image displayImage;

    string activeSentence;
    Image activeImage;
    private GameObject actualCanvas;
    // Start is called before the first frame update
    void Start()
    {
        enemySentences = new Queue<string>();
        enemyImages = new Queue<Image>();
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

    }

    public void GoToLeftPage()
    {

    }

    public void EnemiesPage()
    {
        buttonPanel.SetActive(false);
        enemiesPanel.SetActive(true);
        actualCanvas = enemiesPanel;
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

}
