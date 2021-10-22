using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/DialogObject")]
public class DialogObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] _dialog;
    [SerializeField] private Response[] _responses;

    public string[] Dialog => _dialog;
    public Response[] Responses => _responses;
    public bool HasResponses => Responses != null && Responses.Length > 0;
}
