using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject preFab;
    public GameObject enemyPreFab;
    public GameObject scissorsPreFab;
    public GameObject paperPreFab;

    public GameObject Spawner;
    public Transform bulletSpawn;

    public Transform bulletTrash;
    public Transform paperBulletTrash;
    public Transform scissorsBulletTrash;

    private Enemy _enemy;
    private SpawnEnemy _spawnEnemy;

    // Bullet Types
    private const string _commonBulletType = "common";
    private const string _scissorsBulletType = "scissors";
    private const string _paperBulletType = "paper";

    // Cooldowns
    [SerializeField] private float _shootCommonBulletsCooldown = 0.4f;            // Interval between bullets
    private float _currentCommonBulletsCooldown = 0.4f;                           // Time before next bullet can be shot
    private bool _canShootCommonBullets = true;

    [SerializeField] private float _shootScissorsBulletsCooldown = 2.5f;            // Interval between bullets
    private float _currentScissorsBulletsCooldown = 2.5f;                           // Time before next bullet can be shot
    private bool _canShootScissorsBullets = true;

    [SerializeField] private float _shootPaperBulletsCooldown = 1.5f;            // Interval between bullets
    private float _currentPaperBulletsCooldown = 1.5f;                           // Time before next bullet can be shot
    private bool _canShootPaperBullets = true;

    private Queue<GameObject> _bulletPool = new Queue<GameObject>();            // Stores all common bullets available for shooting
    private Queue<GameObject> _scissorsBulletPool = new Queue<GameObject>();    // Stores all scissors bullets available for shooting
    private Queue<GameObject> _paperBulletPool = new Queue<GameObject>();    // Stores all paper bullets available for shooting
    private void Start()
    {
        _currentCommonBulletsCooldown = _shootCommonBulletsCooldown;
        _currentCommonBulletsCooldown = _shootScissorsBulletsCooldown;

        InitializePool(20, _commonBulletType); // Initialize the pool with 10 bullets
        InitializePool(10, _scissorsBulletType);
        InitializePool(5, _paperBulletType);

        _enemy = enemyPreFab.GetComponent<Enemy>();
        _enemy.SetPlayerShoot(this);

        _spawnEnemy = Spawner.GetComponent<SpawnEnemy>();
        _spawnEnemy.SetPlayerShoot(this);
    }

    private void Update()
    {
        // Time interval between shooting bullets
        handleBulletCoolDown();

        // Shooting 
        if (Input.GetKeyDown(KeyCode.Mouse0)) // && _canShootCommonBullets == true)
        {
            shoot(_commonBulletType);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && _canShootScissorsBullets == true)
        {
            shoot(_scissorsBulletType);
        }
        if (Input.GetKeyDown(KeyCode.Space) && _canShootPaperBullets == true)
        {
            shoot(_paperBulletType);
        }
    }

    private void InitializePool(int poolSize, string bulletType)
    {
        switch (bulletType)
        {
            case _commonBulletType :
                for (int i = 0; i < poolSize; i++)
                {
                    GameObject bullet = Instantiate(preFab); // Creates a new bullet
                    bullet.transform.SetParent(bulletTrash);
                    bullet.SetActive(false);
                    _bulletPool.Enqueue(bullet); // Adds bullet to the pool
                }
            break;
            case _scissorsBulletType :
                for (int i = 0; i < poolSize; i++)
                {
                    GameObject ScissorsBullet = Instantiate(scissorsPreFab); // Creates a new bullet
                    ScissorsBullet.transform.SetParent(scissorsBulletTrash);
                    ScissorsBullet.SetActive(false);
                    _scissorsBulletPool.Enqueue(ScissorsBullet); // Adds bullet to the pool
                }
            break;
            case _paperBulletType:
                for (int i = 0; i < poolSize; i++)
                {
                    GameObject paperBullet = Instantiate(paperPreFab); // Creates a new bullet
                    paperBullet.transform.SetParent(paperBulletTrash);
                    paperBullet.SetActive(false);
                    _paperBulletPool.Enqueue(paperBullet); // Adds bullet to the pool
                }
                break;
        }
        
    }

    private void handleBulletCoolDown()
    {
        if (!_canShootCommonBullets)
        {
            _currentCommonBulletsCooldown -= Time.deltaTime;

            if (_currentCommonBulletsCooldown < 0)
            {
                _canShootCommonBullets = true;
                _currentCommonBulletsCooldown = _shootCommonBulletsCooldown;
            }
        }

        if (!_canShootScissorsBullets)
        {
            _currentScissorsBulletsCooldown -= Time.deltaTime;

            if (_currentScissorsBulletsCooldown < 0)
            {
                _canShootScissorsBullets = true;
                _currentScissorsBulletsCooldown = _shootScissorsBulletsCooldown;
            }
        }

        if (!_canShootPaperBullets)
        {
            _currentPaperBulletsCooldown -= Time.deltaTime;

            if (_currentPaperBulletsCooldown < 0)
            {
                _canShootPaperBullets = true;
                _currentPaperBulletsCooldown = _shootPaperBulletsCooldown;
            }
        }
    }

    private void shoot(string bulletType)
    {
        switch (bulletType)
        {
            case _commonBulletType:
                if (_bulletPool.Count > 0)
                {
                    GameObject bullet = _bulletPool.Dequeue();
                    bullet.transform.position = bulletSpawn.position;
                    bullet.SetActive(true);
                    bullet.transform.SetParent(bulletTrash);

                    // Pass the PlayerShoot reference to the bullet
                    Bullet bulletScript = bullet.GetComponent<Bullet>();
                    if (bulletScript != null) bulletScript.SetPlayerShoot(this); // Pass the reference

                    _canShootCommonBullets = false;
                }
                else
                {
                    Debug.Log("Bullet Pool is Empty!");
                }
                break;
            case _scissorsBulletType :
                if (_scissorsBulletPool.Count > 0)
                {
                    GameObject scissorsBullet = _scissorsBulletPool.Dequeue();
                    scissorsBullet.transform.position = bulletSpawn.position;
                    scissorsBullet.SetActive(true);
                    scissorsBullet.transform.SetParent(scissorsBulletTrash);

                    // Pass the PlayerShoot reference to the bullet
                    ScissorsBullet scissorsBulletScript = scissorsBullet.GetComponent<ScissorsBullet>();
                    if (scissorsBulletScript != null) scissorsBulletScript.SetPlayerShoot(this); // Pass the reference

                    _canShootScissorsBullets = false;
                }
                else
                {
                    Debug.Log("Scissors Bullet Pool is Empty!");
                }
                break;
            case _paperBulletType :
                if (_paperBulletPool.Count > 0)
                {
                    GameObject paperBullet = _paperBulletPool.Dequeue();
                    paperBullet.transform.position = bulletSpawn.position;
                    paperBullet.SetActive(true);
                    paperBullet.transform.SetParent(paperBulletTrash);

                    // Pass the PlayerShoot reference to the bullet
                    PaperBullet paperBulletScript = paperBullet.GetComponent<PaperBullet>();
                    if (paperBulletScript != null) paperBulletScript.SetPlayerShoot(this); // Pass the reference

                    _canShootPaperBullets = false; // needed if there's a cooldown
                }
                else
                {
                    Debug.Log("Paper Bullet Pool is Empty!");
                }
                break;
        }
    }

    public void returnBulletToPool(GameObject bullet, string bulletType)
    {
        Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
        if (rigidbody2D != null) rigidbody2D.velocity = Vector2.zero;

        bullet.SetActive(false);

        switch (bulletType)
        {
            case _commonBulletType :
                    _bulletPool.Enqueue(bullet);
                break;
            case _scissorsBulletType :
                _scissorsBulletPool.Enqueue(bullet);
                break;
            case _paperBulletType:
                _paperBulletPool.Enqueue(bullet);
                break;
        }
    }

    //===========================================================================================
    // GETTER AND SETTER
    //===========================================================================================
    public float accessCommonBulletCooldown
    {
        get { return _shootCommonBulletsCooldown; }
        set { _shootCommonBulletsCooldown = value; }
    }
    public float accessScissorsBulletCooldown
    {
        get { return _shootScissorsBulletsCooldown; }
        set { _shootScissorsBulletsCooldown = value; }
    }
}
