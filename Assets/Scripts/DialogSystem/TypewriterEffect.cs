using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float textSpeed = 50f; 
    public Coroutine Run(string textToType, TMP_Text label)
    {
        return StartCoroutine(TypeText(textToType, label));
    }
    
    private IEnumerator TypeText(string textToType, TMP_Text label)
    {
        var t = 0.0f;
        var charIndex = 0;

        label.text = String.Empty;

        while (charIndex < textToType.Length)
        {
            t += Time.deltaTime * textSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            label.text = textToType.Substring(0, charIndex);

            yield return null;
        }

        label.text = textToType;
    }
}
