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
    [SerializeField] private PlayerShoot _playerShoot;
    [SerializeField] private SwitchBulletType _switchBulletType;

    private bool _shouldCheckAWSD = false;
    private bool _shouldCheckMouse = false;
    private bool _shouldCheckShoot = false;

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
        _playerShoot.enabled = false;
        _switchBulletType.enabled = false;
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

    // Mouse tutorial
    private void StartMouseTutorial()
    {
        _tutorialView.SetText("Use MOUSE to turn");
        _tutorialView.SetVisible(true);
        _shouldCheckMouse = true;
    }

    private IEnumerator EndMouseTutorial()
    {
        yield return StartCoroutine(Wait(3f));
        yield return StartCoroutine(StartShootTutorial());
        //StartAWSDTutorial();
    }

    // Shooting tutorial
    private IEnumerator StartShootTutorial()
    {
        _tutorialView.SetText("!!!WARNING!!!");
        yield return StartCoroutine(Wait(1.5f));
        _tutorialView.SetText("This scissors-enemy is trying to attack you!\nPress LEFT MOUSE KEY to shoot!");
        _playerShoot.enabled = true;
        _shouldCheckShoot = true;
        _tutorialView.ShootTutorial();
    }

    private IEnumerator EndShootTutorial()
    {
        yield return StartCoroutine(Wait(3f));
    }

    // Shooting and killing scissors enemy tutorial

    private IEnumerator EndKillTutorial()
    {
        yield return StartCoroutine(Wait(3f));
    }

    // Obtaining paper bullet and not being able to reahc next enemy

    // Movement tutorial
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

        if (_shouldCheckShoot)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _tutorialView.SetText("Well done!");
                _shouldCheckShoot = false;
                StartCoroutine(EndShootTutorial());
            }
        }
    }
}
