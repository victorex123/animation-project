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

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if(sentences.Count<=0)
        {
            displayText.text = activeSentence;
            return;
        }

        activeSentence = sentences.Dequeue();
        displayText.text = activeSentence;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            dialoguePanel.SetActive(true);
            StartDialogue();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("Player")) && Input.GetKeyDown(KeyCode.E))
        {
            DisplayNextSentence();
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
