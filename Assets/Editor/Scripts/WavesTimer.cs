using UnityEngine;
using System.Collections;

public class WavesTimer : MonoBehaviour
{
    [SerializeField] TutorialModel _tutorialModel;

    public float TimeElapsed { get; private set; }
    private float _targetTime;
    private bool _isRunning = false;

    public void StartTimer(float time)
    {
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

        _tutorialModel.NextTutorial();
        _isRunning = false; // Stop when the timer reaches the target
    }
}