using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform _responseBox;
    [SerializeField] private RectTransform _responseButtonTemplate;
    [SerializeField] private RectTransform _responseContainer;

    private DialogUi _dialogUi;
    private List<GameObject> _tempResponseButtons = new List<GameObject>(); 

    private void Start()
    {
        _dialogUi = GetComponent<DialogUi>();
    }

    public void ShowResponses(Response[] responses)
    {
        var responseBoxHeight = 0.0f;

        foreach (var response in responses)
        {
            GameObject responseButton = Instantiate(_responseButtonTemplate.gameObject, _responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));

            _tempResponseButtons.Add(responseButton);

            responseBoxHeight += _responseButtonTemplate.sizeDelta.y;
        }

        _responseBox.sizeDelta = new Vector2(_responseBox.sizeDelta.x, responseBoxHeight);
        _responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response)
    {
        _responseBox.gameObject.SetActive(false);

        foreach (var button in _tempResponseButtons)
        {
            Destroy(button);
        }
        _tempResponseButtons.Clear();

        _dialogUi.ShowDialog(response.DialogObject);
    }
}
