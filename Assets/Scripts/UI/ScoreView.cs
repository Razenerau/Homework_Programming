using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    public static ScoreView Instance; 
    [SerializeField] private GameObject _preFab;
    private GameObject[] _UIpreFabList = new GameObject[15];
    int howManyPulsing;

    private RectTransform _rectTransform;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        for (int i = 0; i < 15;  i++)
        {
            GameObject like = InitializePreFab(i);

            SetOpacity(like, 0f);
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
        float duration = 0.5f;
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

    public IEnumerator FadeMultiple(int times)
    {
        if (howManyPulsing == 0)
        {
            howManyPulsing++;

            for (int i = 0; i < times; i++)
            {
                StartCoroutine(Fade(_UIpreFabList[i]));
                yield return new WaitForSeconds(0.5f);
            }

            howManyPulsing--;
        }
        else if (howManyPulsing == 1)
        {
            howManyPulsing++;

            for (int i = 0; i < times; i++)
            {
                StartCoroutine(Fade(_UIpreFabList[i + 3]));
                yield return new WaitForSeconds(0.5f);
            }

            howManyPulsing--;
        }
        else if (howManyPulsing == 2)
        {
            howManyPulsing++;

            for (int i = 0; i < times; i++)
            {
                StartCoroutine(Fade(_UIpreFabList[i + 6]));
                yield return new WaitForSeconds(0.5f);
            }

            howManyPulsing--;
        }
        else if (howManyPulsing == 3)
        {
            howManyPulsing++;

            for (int i = 0; i < times; i++)
            {
                StartCoroutine(Fade(_UIpreFabList[i + 9]));
                yield return new WaitForSeconds(0.5f);
            }

            howManyPulsing--;
        }
        else
        {
            howManyPulsing++;

            for (int i = 0; i < times; i++)
            {
                StartCoroutine(Fade(_UIpreFabList[i + 12]));
                yield return new WaitForSeconds(0.5f);
            }

            howManyPulsing--;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FadeMultiple(3));
        }
    }

    public void VisualizeLikes(int times)
    {
        StartCoroutine(FadeMultiple(times));
    }
}
