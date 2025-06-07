using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialVIew : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void SetText(string text)
    {
        _text.text = text;
    }

    public void SetSize(float fontSize)
    {
        _text.fontSize = fontSize; 
    }

    public void SetVisible(bool isvisible)
    {
        Color newColor;
        switch (isvisible)
        {
            case true:
                newColor = new Color(_text.color.r, _text.color.g, _text.color.b, 1f);
                _text.color = newColor;
                break;

            case false:
                newColor = new Color(_text.color.r, _text.color.g, _text.color.b, 0f);
                _text.color = newColor;
                break;
        }

    }
  
}
