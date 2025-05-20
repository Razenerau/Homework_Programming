using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathScreenStats : MonoBehaviour
{
    [SerializeField] private string InitialText;
    [SerializeField] private TextMeshProUGUI TextMeshPro;
    
    public void SetText(int num)
    {
        TextMeshPro.text = $"{InitialText}: {num}";
    }
}


