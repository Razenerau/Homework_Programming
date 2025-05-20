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
        KillCountManager.Reset();
        StartCoroutine(LoadPrefabAfterDelay(1f));
    }

    // Start of the wave
    private System.Collections.IEnumerator LoadPrefabAfterDelay(float delay)
    {
        AnouncementTextView.SetVisible(false);
        yield return new WaitForSeconds(delay);
        AnouncementTextView.SetVisible(true);

        yield return StartCoroutine(Countdown(delay, 3, 175));
        yield return StartCoroutine(FlashText(delay / 5, "GO GO GO", 3, 175));

        _spawner = Instantiate(_preFab, Vector3.zero, Quaternion.identity);
        StartWave(0);
    }

    private static IEnumerator FlashText(float delay, string text, int repeatTimes, int fontSize)
    {
        for (int i = 0; i < repeatTimes; i++)
        {
            yield return new WaitForSeconds(delay);
            AnouncementTextView.SetText(text , fontSize);
            AnouncementTextView.SetVisible(true);

            yield return new WaitForSeconds(delay);
            AnouncementTextView.SetVisible(false);
        }
    }

    private static IEnumerator DisplayText(float delay, string text, int fontSize)
    {
        AnouncementTextView.SetText(text, fontSize);
        AnouncementTextView.SetVisible(true);

        yield return new WaitForSeconds(delay);
        AnouncementTextView.SetVisible(false);
    }

    private static IEnumerator Countdown(float delay, int startingNum, int fontSize)
    {
        for (int i = 3; i > 0; i--)
        {
            AnouncementTextView.SetText(i + "", fontSize);
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

        if (waveIndex >= 10) { StartCoroutine(TheEnd()); return; }

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
        AnouncementTextView.SetLocation("up"); 

        yield return StartCoroutine(DisplayText(3.5f, $"WAVE {_waveIndex} COMPLETE", 100));
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(FlashText(0.2f, $"WAVE {_waveIndex + 1}", 3, 100));

        if (_waveIndex < 11) StartWave(_waveIndex); 
        else StartCoroutine(TheEnd());
    }

    private IEnumerator TheEnd()
    {
        yield return StartCoroutine(DisplayText(1000, "!!!YOU WIN!!!", 175));
    }

}
