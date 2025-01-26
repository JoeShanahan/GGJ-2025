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
    private Queue<DialogueLine> sentences;
    
    private string customerName;
    public event Action OnDialogueEnded;
    
    public void StartDialogue (DialogueLine[] dialogue)
    {
        dialogueBox.gameObject.SetActive(true);
        sentences = new Queue<DialogueLine>(dialogue);

        DisplayNextSentence();
    }
    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        DialogueLine sentence = sentences.Dequeue();
        
        if (sentence.IsMe)
        {
            nameText.text = "You";
        }
        else
        {
            nameText.text = customerName;
        }

        dialogueText.text = sentence.Text;
    }
    
    void EndDialogue()
    {
        dialogueBox.gameObject.SetActive(false);
        OnDialogueEnded?.Invoke();
        Debug.Log("End of dialogue");
    }
}
