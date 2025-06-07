using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWSDView : MonoBehaviour
{
    // 0 = a; 1 = w; 2 = s; 3 = d;
    [SerializeField] private List<SpriteRenderer> _buttonSpriteRenderers; 

    Color _pressedColor = Color.green;

    public void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }

    public void SetAPressed()
    {
        _buttonSpriteRenderers[0].color = _pressedColor;
    }
    public void SetWPressed()
    {
        _buttonSpriteRenderers[1].color = _pressedColor;
    }
    public void SetSPressed()
    {
        _buttonSpriteRenderers[2].color = _pressedColor;
    }
    public void SetDPressed()
    {
        _buttonSpriteRenderers[3].color = _pressedColor;
    }
}
