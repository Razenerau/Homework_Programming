using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public SFX sfx;

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
    public SpriteColor showCooldown;

    // Cooldowns
    /*
    [SerializeField] private float _shootCommonBulletsCooldown = 0.4f;            // Interval between bullets
    private float _currentCommonBulletsCooldown = 0.4f;                           // Time before next bullet can be shot
    private bool _canShootCommonBullets = true;
    */

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
        if (sfx == null) Debug.LogWarning("sfx not found!"); 
        //_currentCommonBulletsCooldown = _shootCommonBulletsCooldown;
        _currentScissorsBulletsCooldown = _shootScissorsBulletsCooldown;

        InitializePool(20, Structs.BulletType.ROCK); // Initialize the pool with 10 bullets
        InitializePool(10, Structs.BulletType.SCISSORS);
        InitializePool(5, Structs.BulletType.PAPER);

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
            //shoot(Structs.BulletType.ROCK);
            shoot(SwitchBulletType.currentBulletType);
        }
    }

    private void InitializePool(int poolSize, string bulletType)
    {
        switch (bulletType)
        {
            case Structs.BulletType.ROCK:
                for (int i = 0; i < poolSize; i++)
                {
                    GameObject bullet = Instantiate(preFab); // Creates a new bullet
                    bullet.transform.SetParent(bulletTrash);
                    bullet.SetActive(false);
                    _bulletPool.Enqueue(bullet); // Adds bullet to the pool
                }
            break;
            case Structs.BulletType.SCISSORS:
                for (int i = 0; i < poolSize; i++)
                {
                    GameObject ScissorsBullet = Instantiate(scissorsPreFab); // Creates a new bullet
                    ScissorsBullet.transform.SetParent(scissorsBulletTrash);
                    ScissorsBullet.SetActive(false);
                    _scissorsBulletPool.Enqueue(ScissorsBullet); // Adds bullet to the pool
                }
            break;
            case Structs.BulletType.PAPER:
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
        /*
        if (!_canShootCommonBullets)
        {
            _currentCommonBulletsCooldown -= Time.deltaTime;

            if (_currentCommonBulletsCooldown < 0)
            {
                _canShootCommonBullets = true;
                _currentCommonBulletsCooldown = _shootCommonBulletsCooldown;
            }
        }
        */

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
            case Structs.BulletType.ROCK:
                if (_bulletPool.Count > 0)
                {
                    GameObject bullet = _bulletPool.Dequeue();
                    bullet.transform.position = bulletSpawn.position;
                    bullet.SetActive(true);
                    bullet.transform.SetParent(bulletTrash);

                    // Pass the PlayerShoot reference to the bullet
                    Bullet bulletScript = bullet.GetComponent<Bullet>();
                    if (bulletScript != null) bulletScript.SetPlayerShoot(this); // Pass the reference

                    //_canShootCommonBullets = false;

                    sfx.shootRock();
                }
                else
                {
                    Debug.Log("Bullet Pool is Empty!");
                }
                break;
            case Structs.BulletType.SCISSORS:
                if (_scissorsBulletPool.Count > 0 && _canShootScissorsBullets)
                {
                    GameObject scissorsBullet = _scissorsBulletPool.Dequeue();
                    scissorsBullet.transform.position = bulletSpawn.position;
                    scissorsBullet.SetActive(true);
                    scissorsBullet.transform.SetParent(scissorsBulletTrash);

                    // Pass the PlayerShoot reference to the bullet
                    ScissorsBullet scissorsBulletScript = scissorsBullet.GetComponent<ScissorsBullet>();
                    if (scissorsBulletScript != null) scissorsBulletScript.SetPlayerShoot(this); // Pass the reference

                    _canShootScissorsBullets = false;

                    sfx.shootScissors();
                }
                else
                {
                    Debug.Log("Scissors Bullet Pool is Empty!");
                }
                break;
            case Structs.BulletType.PAPER:
                if (_paperBulletPool.Count > 0 && _canShootPaperBullets)
                {
                    GameObject paperBullet = _paperBulletPool.Dequeue();
                    paperBullet.transform.position = bulletSpawn.position;
                    paperBullet.SetActive(true);
                    paperBullet.transform.SetParent(paperBulletTrash);

                    // Pass the PlayerShoot reference to the bullet
                    PaperBullet paperBulletScript = paperBullet.GetComponent<PaperBullet>();
                    if (paperBulletScript != null) paperBulletScript.SetPlayerShoot(this); // Pass the reference

                    _canShootPaperBullets = false; // needed if there's a cooldown

                    sfx.shootPaper();
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
            case Structs.BulletType.ROCK:
                    _bulletPool.Enqueue(bullet);
                break;
            case Structs.BulletType.SCISSORS:
                _scissorsBulletPool.Enqueue(bullet);
                break;
            case Structs.BulletType.PAPER:
                _paperBulletPool.Enqueue(bullet);
                break;
        }
    }

    //===========================================================================================
    // GETTER AND SETTER
    //===========================================================================================
    /*
    public float accessCommonBulletCooldown
    {
        get { return _shootCommonBulletsCooldown; }
        set { _shootCommonBulletsCooldown = value; }
    }
    */
    public float accessScissorsBulletCooldown
    {
        get { return _shootScissorsBulletsCooldown; }
        set { _shootScissorsBulletsCooldown = value; }
    }
    public float accessPaperBulletCooldown
    {
        get { return _shootPaperBulletsCooldown; }
        set { _shootPaperBulletsCooldown = value; }
    }
}
