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
        Vector2 pos = new Vector3(8, 0, 0);
        Vector2 targetPos = new Vector3(4, 0, 0);
        GameObject scissorsEnemy = Instantiate(_scissorsEnemy, pos, Quaternion.Euler(0, 0, 90), gameObject.transform);
        StartCoroutine(MoveObject(scissorsEnemy, targetPos, 5));
    }

    private IEnumerator MoveObject(GameObject gameObject, Vector3 targetPos, float moveDuration)
    {
        //float threshold = 0.01f;
        /*while (Vector3.Distance(transform.position, targetPos) > threshold)
        {
            Vector3 pos = gameObject.transform.position;
            pos = Vector3.MoveTowards(pos, targetPos, speed * Time.deltaTime);
            yield return null; // Waits for the next frame
        }*/
        Vector3 startPos = gameObject.transform.position;
        float elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }

}
