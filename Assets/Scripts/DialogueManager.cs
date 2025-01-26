
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Transform dialogueBox;
    private Queue<string> sentences;
    
    private string customerName;

    public bool isMe;
    void Start()
    {
        dialogueBox.gameObject.SetActive(false);
        sentences = new Queue<string>();
    }
    public void StartDialogue (Dialogue dialogue)
    {
        dialogueBox.gameObject.SetActive(true);
        string StringSentences = String.Join(",", dialogue.DialogueArray);

        customerName = dialogue.name;

        sentences.Clear();
        foreach (string sentence in dialogue.DialogueArray)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        if (!isMe)
        {
            nameText.text = customerName;
        }
        else
        {
            nameText.text = "You";
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }
    void EndDialogue()
    {
        dialogueBox.gameObject.SetActive(false);
        Debug.Log("End of dialogue");
    }
    public void ChangeCheckbox(bool me){
        isMe = me;
    }
}
