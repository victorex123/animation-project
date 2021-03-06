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
    private bool pressZ;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            pressZ = true;
        }
        //else
        //{
        //    pressE = false;
        //}
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
        i = dialogue.sentenceList.Length;
    }

    private void OnTriggerStay(Collider other)
    {

        if ((other.CompareTag("Player")) && pressZ && !startTalking)
        {
            i--;
            if(i<0)
            {
                dialoguePanel.SetActive(false);
                return;
            }
            DisplayNextSentence();
            pressZ = false;
        }


        if (other.CompareTag("Player") && pressZ && startTalking)
        {
            dialoguePanel.SetActive(true);
            StartDialogue();
            startTalking = false;
            pressZ = false;

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
