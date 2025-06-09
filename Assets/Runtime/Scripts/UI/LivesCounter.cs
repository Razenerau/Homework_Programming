using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesCounter : MonoBehaviour
{
    public PlayerHealth playerHealth;
    private RectTransform _rect;
    private const int _tileWidth = 100;

    private void Awake()
    {
        updateLivesCounter();
    }

    public void updateLivesCounter()
    {
        _rect = transform as RectTransform;
        _rect.sizeDelta = new Vector2(playerHealth.GetHealth() * _tileWidth, _rect.sizeDelta.y);
        _rect.anchoredPosition = new Vector2(playerHealth.GetHealth() * 50, 0);
    }
}
