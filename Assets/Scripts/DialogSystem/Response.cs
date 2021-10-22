using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string _responseText;
    [SerializeField] private DialogObject _dialogObject;

    public string ResponseText => _responseText;

    public DialogObject DialogObject => _dialogObject;
}
