using System.Collections;
using TMPro;
using UnityEngine;



public class DialogUi : MonoBehaviour
{
    [SerializeField] private GameObject _dialogBox;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private DialogObject _testDialog;

    public bool IsOpen { get; private set; }

    private ResponseHandler _responseHandler;
    private TypewriterEffect _typewriterEffect;

    private void Start()
    {
        _typewriterEffect = GetComponent<TypewriterEffect>();
        _responseHandler = GetComponent<ResponseHandler>();
        _dialogBox.SetActive(false);
        CloseDialogBox();
    }

    public void ShowDialog(DialogObject dialogObject)
    {
        IsOpen = true;
        _dialogBox.SetActive(true);
        StartCoroutine(StepThroughDialog(dialogObject));
    }

    private IEnumerator StepThroughDialog(DialogObject dialogObject)
    {
        for (var i = 0; i < dialogObject.Dialog.Length; i++)
        {
            var dialog = dialogObject.Dialog[i];
            yield return _typewriterEffect.Run(dialog, _text);

            if(i == dialogObject.Dialog.Length - 1 && dialogObject.HasResponses){}

            yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
        }

        if (dialogObject.HasResponses)
        {
            _responseHandler.ShowResponses(dialogObject.Responses);
        }
        else
        {
            CloseDialogBox();
        }

    }


    private void CloseDialogBox()
    {
        IsOpen = false;
        _dialogBox.SetActive(false);
        _text.text = string.Empty;
    }
}
