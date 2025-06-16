using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Image image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

        StartCoroutine(Startfade());
    }

    IEnumerator Startfade()
    {
        yield return StartCoroutine(StartButton());
    }
    IEnumerator StartButton()
    {
        for (int i = 0; i <= 10000; i++)
        {
            Image image = GetComponent<Image>();
            image.color = new Color(image.color.r, image.color.g, image.color.b, i/100);
            yield return null;
        }
    }
}
