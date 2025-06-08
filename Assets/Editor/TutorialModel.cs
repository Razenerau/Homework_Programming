using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TutorialModel : MonoBehaviour
{
    [SerializeField] private TutorialVIew _tutorialView;
    [SerializeField] private AWSDView _AWSDView;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerRotation _playerRotation;

    private bool _shouldCheckAWSD = false;
    private bool _shouldCheckMouse = false;

    private bool _isAPressed = false;
    private bool _isWPressed = false;
    private bool _isSPressed = false;
    private bool _isDPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        _tutorialView.SetSize(70f);
        StartCoroutine(TutorialStart());
        _tutorialView.SetVisible(false);
        _AWSDView.SetVisible(false);
        _playerMovement.enabled = false;
    }

    private IEnumerator TutorialStart()
    {
        yield return StartCoroutine(Wait(1f));
        
        StartMouseTutorial();
    }

    private IEnumerator Wait(float duraiton)
    {
        yield return new WaitForSecondsRealtime(duraiton);
    }

    private void StartMouseTutorial()
    {
        _tutorialView.SetText("Use MOUSE to turn");
        _tutorialView.SetVisible(true);
        _shouldCheckMouse = true;
    }

    private IEnumerator EndMouseTutorial()
    {
        yield return StartCoroutine(Wait(3f));
        StartAWSDTutorial();
    }

    private void StartAWSDTutorial()
    {
        _playerMovement.enabled = true;
        _tutorialView.SetText("Use AWSD to move");
        _tutorialView.SetVisible(true);
        _AWSDView.SetVisible(true);
        _shouldCheckAWSD = true;
    }

    private IEnumerator EndAWSDTutorial()
    {
        _tutorialView.SetText("Well done!");
        yield return StartCoroutine(Wait(1f));
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
                StartCoroutine(EndAWSDTutorial());
                _shouldCheckAWSD = false;
            }
        }

        if(_shouldCheckMouse)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            if (mouseX != 0 || mouseY != 0)
            {
                _tutorialView.SetText("Well done!");
                _shouldCheckMouse = false;
                StartCoroutine(EndMouseTutorial());
            }
        }
    }
}
