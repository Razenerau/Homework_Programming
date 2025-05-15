using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private SpawnEnemy _preFab;
    [SerializeField] private int _waveIndex;
    
    private SpawnEnemy _spawner;
    public bool CanSpawn = false;


    public enum WaveType
    {
        ROCK,
        PAPER,
        SCISSORS,
        ROCK_SCISSORS,
        ROCK_PAPER,
        PAPER_SCISSORS,
        ALL

    }
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(LoadPrefabAfterDelay(1f));
    }

    // Start of the wave
    private System.Collections.IEnumerator LoadPrefabAfterDelay(float delay)
    {
        StartCounterView.SetVisible(false);
        yield return new WaitForSeconds(delay);
        StartCounterView.SetVisible(true);

        yield return StartCoroutine(Countdown(delay, 3));
        yield return StartCoroutine(FlashText(delay / 5, "GO GO GO", 3));

        _spawner = Instantiate(_preFab, Vector3.zero, Quaternion.identity);
        StartWave(0);
    }

    private static IEnumerator FlashText(float delay, string text, int repeatTimes)
    {
        for (int i = 0; i < repeatTimes; i++)
        {
            yield return new WaitForSeconds(delay);
            StartCounterView.SetText(text);
            StartCounterView.SetVisible(true);

            yield return new WaitForSeconds(delay);
            StartCounterView.SetVisible(false);
        }
    }

    private static IEnumerator DisplayText(float delay, string text)
    {
        StartCounterView.SetText(text);
        StartCounterView.SetVisible(true);

        yield return new WaitForSeconds(delay);
        StartCounterView.SetVisible(false);
    }

    private static IEnumerator Countdown(float delay, int startingNum)
    {
        for (int i = 3; i > 0; i--)
        {
            StartCounterView.SetText(i + "");
            yield return new WaitForSeconds(delay);
        }
    }

    private System.Collections.IEnumerator WaveDuration(float duration, WaveType type)
    {
        float timer = 0f;

        while (timer < duration)
        {
            _spawner.EnemySpawn(type); 
            timer += Time.deltaTime;
            yield return null; // Wait for next frame
        }

        // Run end wave routine
        StartCoroutine(EndWave());
    }

    private void StartWave(int waveIndex)
    {
        List<Wave> wavesList = WaveList.Instance.WavesList;

        int maxEnemyCount = wavesList[waveIndex].MaxEnemyCount;
        float duration = wavesList[waveIndex].Duration;
        WaveType type = wavesList[waveIndex].Type;

        _spawner.maxEnemyCount = maxEnemyCount;
        StartCoroutine(WaveDuration(duration, type));
    }

    private IEnumerator EndWave()
    {
        _waveIndex++;

        KillCountManager.UpdateData();

        yield return StartCoroutine(DisplayText(3.5f, $"WAVE {_waveIndex} COMPLETE"));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(FlashText(0.2f, $"WAVE {_waveIndex + 1}", 3));
        StartWave(_waveIndex);  
    }

}
