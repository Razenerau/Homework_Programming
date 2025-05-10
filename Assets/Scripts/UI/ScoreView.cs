using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private GameObject _preFab;
    private GameObject[] _UIpreFabList = new GameObject[3];

    private RectTransform _rectTransform;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        for (int i = 0; i < 3;  i++)
        {
            GameObject like = InitializePreFab(i);

            SetOpacity(like, 0f);
        }
    }

    private GameObject InitializePreFab(int i)
    {
        GameObject like = Instantiate(_preFab, _rectTransform.position, Quaternion.identity);
        like.name = $"Like_{i}";
        like.transform.SetParent(_rectTransform);
        like.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        _UIpreFabList[i] = like;
        return like;
    }

    private static void SetOpacity(GameObject like, float a)
    {
        Image image = like.GetComponent<Image>();
        Color newColor = image.color;
        newColor.a = a;
        image.color = newColor;
    }

    private System.Collections.IEnumerator Fade(GameObject gameObject)
    {
        gameObject.transform.position = _rectTransform.position;
            
        float elapsed = 0f;
        float duration = 1f;
        SetOpacity(gameObject, 1f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            SetOpacity(gameObject, alpha);

            float yPos = gameObject.transform.position.y;
            yPos += elapsed * 0.5f;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, yPos, 0);
            yield return null;
        }
    }

    private IEnumerator FadeMultiple(int times)
    {
        for (int i = 0; i < times; i++)
        {
            StartCoroutine(Fade(_UIpreFabList[i]));
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FadeMultiple(3));
            //StartCoroutine(Fade(_UIpreFabList[0]));
        }
    }
}
