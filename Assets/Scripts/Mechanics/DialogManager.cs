using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogManager : MonoBehaviour
{
    private Queue<string> sentences;
    public Label nameText;
    public Label dialogText;
    void Start()
    {
        sentences = new Queue<string>();
        
    }

    public void StartDialog(Dialog dialog)
    {
        nameText.text = dialog.name;

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
        dialogText.text = sentence;
    }

    private void EndDialog()
    {
        Debug.Log("Conversation over");
    }
}
