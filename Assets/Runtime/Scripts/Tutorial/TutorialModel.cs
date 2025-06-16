using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TutorialModel : MonoBehaviour
{
    [SerializeField] private SpawnerManager _spawnerManager;
    [SerializeField] private TutorialVIew _tutorialView;
    [SerializeField] private AWSDView _AWSDView;
    [SerializeField] private SpawnEnemy _spawnEnemy;
    [SerializeField] private GameController _gameController;
    [SerializeField] private WavesTimer _wavesTimer;
    [SerializeField] private MenuManager _menuManager;

    [Header("Player")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerRotation _playerRotation;
    [SerializeField] private PlayerShoot _playerShoot;
    [SerializeField] private SwitchBulletType _switchBulletType;
    [SerializeField] private KeepPositionStatic _keepPositionStatic;
    [SerializeField] private PlayerHealth _playerHealth;
   

    private int _tutorialIndex = 0;
    private int _tempScore = 0;

    private bool _shouldCheckAWSD = false;
    private bool _shouldCheckMouse = false;
    private bool _shouldCheckScissorsEnemyDeath = false;
    private bool _shouldCheckPlayerHealth = false;
    private bool _shouldCheckRockEnemyDeath = false;

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
        ShootTutorial(Structs.BulletType.SCISSORS);
        yield return StartCoroutine(Wait(1.5f));
        _tutorialView.SetText("This scissors-enemy is trying to attack you!\nPress LEFT MOUSE KEY to shoot!");
        _playerShoot.enabled = true;
        _shouldCheckScissorsEnemyDeath = true;
        
    }

    public void ShootTutorial(string enemyType)
    {
        BoundsView _boundsView = GetComponent<BoundsView>();
        _boundsView.SetBoundsActive(false);

        Vector3 pos = new Vector3(12, 0, 0);
        Vector3 targetPos = new Vector3(4, 0, 0);

        GameObject enemy;

        switch (enemyType)
        {
            case Structs.BulletType.ROCK:
                enemy = _tutorialView.InstanciateRockEnemy(pos, Quaternion.Euler(0, 0, 90), gameObject.transform);
                Enemy rockScript = enemy.GetComponent<Enemy>();
                rockScript.deathTime = 999999f;
                break;
            case Structs.BulletType.SCISSORS:
                enemy = _tutorialView.InstanciateScissorsEnemy(pos, Quaternion.Euler(0, 0, 90), gameObject.transform);
                ScissorsEnemy scissrosScript = enemy.GetComponent<ScissorsEnemy>();
                scissrosScript.deathTime = 999999f;
                break;
            default: // creates rock even tho its supposed to be paper
                enemy = _tutorialView.InstanciateRockEnemy(pos, Quaternion.Euler(0, 0, 90), gameObject.transform);
                Enemy paperScript = enemy.GetComponent<Enemy>();
                paperScript.deathTime = 999999f;
                break;
        }

        StartCoroutine(MoveObject(enemy, targetPos, 100));
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

        BoundsView _boundsView = GetComponent<BoundsView>();
        _boundsView.SetBoundsActive(true);
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

        _wavesTimer.StartTimer(20f); 

        yield return null;
    }

    private IEnumerator TryAgain()
    {
        _wavesTimer.StopTimer();
        _spawnerManager.gameObject.SetActive(false);
        _spawnEnemy.gameObject.SetActive(false);
        _tutorialView.SetText("You were so close!");
        yield return StartCoroutine(Wait(3f));
        _tutorialView.SetText("Let's try again!");
        _playerHealth.Health = 5;
        _shouldCheckPlayerHealth = true;
        yield return StartCoroutine(Wait(2f));

        switch (_tutorialIndex)
        {
            case 0:
                _spawnerManager.gameObject.SetActive(true);
                _spawnEnemy.gameObject.SetActive(true);
                _spawnerManager.StartWave(0);
                _gameController.SetScore(0);
                _wavesTimer.StartTimer(20f);
                break;
            case 1:
                _spawnerManager.gameObject.SetActive(true);
                _spawnEnemy.gameObject.SetActive(true);
                _spawnerManager.StartWave(1);
                _gameController.SetScore(_tempScore);
                _wavesTimer.StartTimer(20f);
                break;
        }
    }

    private IEnumerator EndHarderShootTutorial()
    {
        _tutorialView.SetText("Well done!");
        _shouldCheckPlayerHealth = false;
        yield return StartCoroutine(Wait(3f));

        StartCoroutine(UnlockOtherBullets());
    }

    private IEnumerator UnlockOtherBullets()
    {
        _tutorialView.SetText("Your fingers aren't glued together...\nPress RIGHT MOUSE KEY to change their position!");
        _switchBulletType.enabled = true;

        while (!Input.GetKeyDown(KeyCode.Space) && !Input.GetKeyDown(KeyCode.Mouse1))
        {
            yield return null;
        }
        _tutorialView.SetText("Well done!");
        ShootTutorial(Structs.BulletType.ROCK);
        yield return StartCoroutine(Wait(3f));
        _tutorialView.SetText("Try to defeat this enemy by choosing PAPER!");
        _shouldCheckRockEnemyDeath = true;
        StartCoroutine(StartAWSDTutorial());
    }

    

    public void NextTutorial()
    {
        switch (_tutorialIndex)
        {
            case 0:
                StartCoroutine(EndHarderShootTutorial());
                break;
            case 1:
                StartCoroutine(EndLastTutorial());
                break;
        }

        _tutorialIndex++;
    }

    // ----------------------------------------------------
    // Movement tutorial
    // ----------------------------------------------------

    private IEnumerator StartAWSDTutorial()
    {
        while (!Input.GetKeyDown(KeyCode.Mouse0) || SwitchBulletType.currentBulletType != Structs.BulletType.PAPER)
        {
            yield return null;
        }

        _tutorialView.SetText("Oops... I forgot you can't move...");
         yield return StartCoroutine(Wait(5f));

        _keepPositionStatic.enabled = false;
        _playerMovement.enabled = true;
        _tutorialView.SetText("Use AWSD to move");
        _tutorialView.SetVisible(true);
        _AWSDView.SetVisible(true);
        _shouldCheckAWSD = true;
    }

    private IEnumerator EndAWSDTutorial()
    {
        _tutorialView.SetText("That's better!");
        yield return StartCoroutine(Wait(1f));
        _AWSDView.SetVisible(false);
    }

    // ----------------------------------------------------
    // Last tutorial
    // ----------------------------------------------------

    private IEnumerator LastTutorial()
    {
        _tempScore = _gameController.GetScore();
        _tutorialView.SetText("Let's see how well you can do!");
        yield return StartCoroutine(Wait(3f));
        BoundsView boundsView = GetComponent<BoundsView>();
        boundsView.SetBoundsActive(true);
        _shouldCheckPlayerHealth = true;

        _spawnerManager.StartWave(1);

        _wavesTimer.StartTimer(20f);

        yield return null;
    }

    private IEnumerator EndLastTutorial()
    {
        _tutorialView.SetText("Good Job!");
        yield return StartCoroutine(Wait(3f));
        _tutorialView.SetText("Now you're ready for the game!");
        yield return StartCoroutine(Wait(3f));
        _tutorialView.SetText("Press ENTER whenever you're ready");

        while (!Input.GetKeyDown(KeyCode.Return) && !Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            yield return null;
        }

        _menuManager.LoadNextScene();
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

        if (_shouldCheckScissorsEnemyDeath)
        {
            if (gameObject.transform.childCount == 0)
            {
                _tutorialView.SetText("Well done!");
                _shouldCheckScissorsEnemyDeath = false;
                StartCoroutine(EndShootTutorial());
            }
        }

        if (_shouldCheckRockEnemyDeath)
        {
            if (gameObject.transform.childCount == 0)
            {
                _tutorialView.SetText("Well done!");
                _shouldCheckRockEnemyDeath = false;
                StartCoroutine(LastTutorial());

                // Make sure that awsd button dont appear
                _AWSDView.SetVisible(false);
                _shouldCheckAWSD = false;

            }
        }

        if (_shouldCheckPlayerHealth)
        {
            if(_playerHealth.GetHealth() <= 1)
            {
                _shouldCheckPlayerHealth = false;
                StartCoroutine(TryAgain());
            }
        }
    }
}
