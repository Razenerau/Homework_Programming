using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AnouncementTextView : MonoBehaviour
{
    public static AnouncementTextView Instance;
    private static TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = "3";
        _text.fontSize = 175;
    }

    public static void SetLocation(string type)
    {
        Vector3 pos;

        switch (type)
        {
            case "up":
                pos = new Vector3(_text.rectTransform.position.x, 255, 0);
                break;
            default:
                pos = new Vector3(_text.rectTransform.position.x, -115, 0);
                break;
        }

        _text.rectTransform.position = pos;
    }

    public static void SetText(string text)
    {
        _text.text = text;
    }

    public static void SetText(string text, float fontSize)
    {
        _text.text = text;
        _text.fontSize = fontSize;  // 175 or 100
    }

    public static void SetVisible(bool isvisible)
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
