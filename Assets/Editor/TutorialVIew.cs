using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialVIew : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _scissorsEnemy;

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

    public void ShootTutorial()
    {
        Vector3 pos = new Vector3(12, 0, 0);
        Vector3 targetPos = new Vector3(4, 0, 0);
        GameObject scissorsEnemy = Instantiate(_scissorsEnemy, pos, Quaternion.Euler(0, 0, 90), gameObject.transform);
        
        ScissorsEnemy script = scissorsEnemy.GetComponent<ScissorsEnemy>();
        script.deathTime = 999999f;

        StartCoroutine(MoveObject(scissorsEnemy, targetPos, 100));
    }

    private IEnumerator MoveObject(GameObject gameObject, Vector3 targetPos, float moveDuration)
    {
        
        Vector3 startPos = gameObject.transform.position;
        float elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            float startingX = gameObject.transform.position.x;
            float finalX = targetPos.x;
            float slope = (finalX - startingX) / moveDuration;
            float currentX = slope * elapsedTime + startingX;

            gameObject.transform.position = new Vector3(currentX, 0, 0);
            elapsedTime += Time.deltaTime * 0.1f;
            yield return null;
        }

    }

}
