using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Dialogue dialogue;

    Queue<string> sentences;

    public GameObject dialoguePanel;
    public Text displayText;

    string activeSentence;
    bool startTalking = false;
    private int i;
    private bool pressE;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            pressE = true;
        }
        else
        {
            pressE = false;
        }
    }

    public void StartDialogue()
    {
        sentences.Clear();

        foreach(string sentence in dialogue.sentenceList)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count <= 0)
        {
            return;
        }

        activeSentence = sentences.Dequeue();
        displayText.text = activeSentence;
    }

    private void OnTriggerEnter(Collider other)
    {
        startTalking = true;
    }

    private void OnTriggerStay(Collider other)
    {

        if ((other.CompareTag("Player")) && pressE && !startTalking)
        {
            i++;
            if(i>dialogue.sentenceList.Length)
            {
                dialoguePanel.SetActive(false);
                return;
            }
            DisplayNextSentence();
        }


        if (other.CompareTag("Player") && pressE && startTalking)
        {
            dialoguePanel.SetActive(true);
            StartDialogue();
            startTalking = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialoguePanel.SetActive(false);
        }
    }
}
