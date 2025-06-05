using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCountdownModel : MonoBehaviour
{
    private WaveCountdownView waveCountdownView;
    public static WaveCountdownModel Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        waveCountdownView = GetComponent<WaveCountdownView>();
    }

    public IEnumerator Countdown(float duration)
    {
        for (int i = (int)duration; i >= 0; i--)
        {
            waveCountdownView.SetText(i);
            yield return new WaitForSeconds(1);
        }

        waveCountdownView.SetText("");
    }
}
