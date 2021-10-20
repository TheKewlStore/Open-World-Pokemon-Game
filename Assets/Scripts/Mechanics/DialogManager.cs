using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private Queue<string> sentences;
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialog(Dialog dialog)
    {
        Debug.Log($"Starting conversation with {dialog.name}");

        sentences.Clear();

        foreach (var sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
    }

    private void EndDialog()
    {
        Debug.Log("Conversation over");
    }
}
