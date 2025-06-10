using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.PlayerSettings;

public class TutorialVIew : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _scissorsEnemy;
    [SerializeField] private GameObject _rockEnemy;

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

    public GameObject InstanciateScissorsEnemy(Vector3 pos, Quaternion rotation, Transform transform)
    {
        GameObject scissorsEnemy = Instantiate(_scissorsEnemy, pos, rotation, transform);
        return scissorsEnemy;
    }

    public GameObject InstanciateRockEnemy(Vector3 pos, Quaternion rotation, Transform transform)
    {
        GameObject rockEnemy = Instantiate(_rockEnemy, pos, rotation, transform);
        Debug.Log(rockEnemy.transform.position + " " +  rockEnemy.name);
        return rockEnemy;
    }

    private void SetPosition(GameObject gameObject, Vector3 pos)
    {
        gameObject.transform.position = pos;
    }

}
