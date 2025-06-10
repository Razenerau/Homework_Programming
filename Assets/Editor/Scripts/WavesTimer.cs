using UnityEngine;
using System.Collections;

public class WavesTimer : MonoBehaviour
{
    public float TimeElapsed { get; private set; }
    private float _targetTime;
    private bool _isRunning = false;

    public void StartTimer(float time)
    {
        Debug.Log("Timer started");
        _targetTime = time;
        TimeElapsed = 0;
        _isRunning = true;
        StartCoroutine(TimerCoroutine());
    }

    public void StopTimer()
    {
        _isRunning = false;
    }

    public void ResetTimer()
    {
        TimeElapsed = 0;
        _isRunning = false;
    }

    private IEnumerator TimerCoroutine()
    {
        while (_isRunning && TimeElapsed < _targetTime)
        {
            TimeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        Debug.Log("Time has ran out0");
        _isRunning = false; // Stop when the timer reaches the target
    }
}