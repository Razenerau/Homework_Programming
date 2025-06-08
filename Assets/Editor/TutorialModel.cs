using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TutorialModel : MonoBehaviour
{
    [SerializeField] private TutorialVIew _tutorialView;
    [SerializeField] private AWSDView _AWSDView;

    private bool _shouldCheckAWSD = false;
    private bool _isAPressed = false;
    private bool _isWPressed = false;
    private bool _isSPressed = false;
    private bool _isDPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TutorialStart());
        _tutorialView.SetVisible(false);
        _AWSDView.SetVisible(false);
    }

    private IEnumerator TutorialStart()
    {
        yield return new WaitForSecondsRealtime(1f);
        StartAWSD();
    }

    private IEnumerator Wait(float duraiton)
    {
        yield return new WaitForSecondsRealtime(duraiton);
    }

    private void StartAWSD()
    {
        _tutorialView.SetSize(70f);
        _tutorialView.SetText("Use StartAWSD to move");
        _tutorialView.SetVisible(true);
        _AWSDView.SetVisible(true);
        _shouldCheckAWSD = true;
    }

    private void EndAWSD()
    {
        _tutorialView.SetText("Well done!");
        StartCoroutine(Wait(1f));
        _AWSDView.SetVisible(false);
    }



    private void Update()
    {
        if (_shouldCheckAWSD)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _isAPressed = true;
                _AWSDView.SetAPressed();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                _isWPressed = true;
                _AWSDView.SetWPressed();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                _isSPressed = true;
                _AWSDView.SetSPressed();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _isDPressed = true;
                _AWSDView.SetDPressed();
            }

            if(_isAPressed && _isWPressed && _isSPressed && _isDPressed)
            {
                EndAWSD();
            }
        }
    }
}
