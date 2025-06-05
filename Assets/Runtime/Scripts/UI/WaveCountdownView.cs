using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveCountdownView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro; 

    public void SetText(int num)
    {
        textMeshPro.text = num.ToString();
    }

    public void SetText(string text)
    {
        textMeshPro.text = text;
    }
}
