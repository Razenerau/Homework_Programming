using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TutorialModel : MonoBehaviour
{
    [SerializeField] private SpawnerManager _spawnerManager;
    [SerializeField] private TutorialVIew _tutorialView;
    [SerializeField] private AWSDView _AWSDView;

    [Header("Player")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerRotation _playerRotation;
    [SerializeField] private PlayerShoot _playerShoot;
    [SerializeField] private SwitchBulletType _switchBulletType;
    [SerializeField] private KeepPositionStatic _keepPositionStatic;
    [SerializeField] private PlayerHealth _playerHealth;
   

    

    private bool _shouldCheckAWSD = false;
    private bool _shouldCheckMouse = false;
    private bool _shouldCheckScissorsDeath = false;
    private bool _shouldCheckPlayerHealth = false;

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

    // ----------------------------------------------------
    // Mouse tutorial
    // ----------------------------------------------------

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

    // ----------------------------------------------------
    // Shooting tutorial
    // ----------------------------------------------------

    private IEnumerator StartShootTutorial()
    {
        _tutorialView.SetText("! ! ! WARNING ! ! !");
        ShootTutorial();
        yield return StartCoroutine(Wait(1.5f));
        _tutorialView.SetText("This scissors-enemy is trying to attack you!\nPress LEFT MOUSE KEY to shoot!");
        _playerShoot.enabled = true;
        _shouldCheckScissorsDeath = true;
        
    }

    public void ShootTutorial()
    {
        Vector3 pos = new Vector3(12, 0, 0);
        Vector3 targetPos = new Vector3(4, 0, 0);

        GameObject scissorsEnemy = _tutorialView.InstanciateScissorsEnemy(pos, Quaternion.Euler(0, 0, 90), gameObject.transform);

        ScissorsEnemy script = scissorsEnemy.GetComponent<ScissorsEnemy>();
        script.deathTime = 999999f;

        StartCoroutine(MoveObject(scissorsEnemy, targetPos, 100));
    }

    private IEnumerator MoveObject(GameObject gameObject, Vector3 targetPos, float moveDuration)
    {
        if (gameObject != null)
        {
            Vector3 startPos = gameObject.transform.position;
            float elapsedTime = 0;
            while (elapsedTime < moveDuration)
            {
                if (gameObject == null)
                {
                    Debug.Log("Scissors were destroyed");
                    break;
                }

                float startingX = gameObject.transform.position.x;
                float finalX = targetPos.x;
                float slope = (finalX - startingX) / moveDuration;
                float currentX = slope * elapsedTime + startingX;

                gameObject.transform.position = new Vector3(currentX, 0, 0);
                elapsedTime += Time.deltaTime * 0.1f;
                yield return null;
            }
        }
    }

    private IEnumerator EndShootTutorial()
    {
        yield return StartCoroutine(Wait(3f));
        StartCoroutine(HarderShootTutorial());
    }

    // ----------------------------------------------------
    // Harer Shooting tutorial
    // ----------------------------------------------------

    private IEnumerator HarderShootTutorial()
    {
        _tutorialView.SetText("Let's make it a bit harder!");
        BoundsView boundsView = GetComponent<BoundsView>();
        boundsView.SetBoundsActive(true);
        _shouldCheckPlayerHealth = true;

        _spawnerManager.ForceInstantiateSpawner();
        _spawnerManager.StartWave(0);

        yield return StartCoroutine(Wait(20f));
    }

    private IEnumerator TryAgain(int stage)
    {
        switch (stage)
        {
            case 0:
                _tutorialView.SetText("You were so close!");
                yield return StartCoroutine(Wait(3f));
                _tutorialView.SetText("Let's try again!");
                _playerHealth.Health = 5;
                _shouldCheckPlayerHealth = true;
                break;
        }
    }

    // Obtaining paper bullet and not being able to reach next enemy

    // ----------------------------------------------------
    // Movement tutorial
    // ----------------------------------------------------

    private void StartAWSDTutorial()
    {
        _keepPositionStatic.enabled = false;
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

        if (_shouldCheckScissorsDeath)
        {
            if (gameObject.transform.childCount == 0)
            {
                _tutorialView.SetText("Well done!");
                _shouldCheckScissorsDeath = false;
                StartCoroutine(EndShootTutorial());
            }
        }

        if (_shouldCheckPlayerHealth)
        {
            if(_playerHealth.GetHealth() <= 1)
            {
                _shouldCheckPlayerHealth = false;
                StartCoroutine(TryAgain(0));
            }
        }
    }
}
