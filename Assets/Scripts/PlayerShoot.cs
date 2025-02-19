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
    public Transform bulletTrash;
    public Transform bulletSpawn;
    public Transform scissorsBulletTrash;

    private Enemy _enemy;
    private SpawnEnemy _spawnEnemy;

    // Bullet Types
    private const string _commonBulletType = "common";
    private const string _scissorsBulletType = "scissors";

    [SerializeField] private float _shootCommonBulletsCooldown = 0.5f;            // Interval between bullets
    private float _currentCommonBulletsCooldown = 0.5f;                           // Time before next bullet can be shot
    private bool _canShootCommonBullets = true;

    [SerializeField] private float _shootScissorsBulletsCooldown = 2.5f;            // Interval between bullets
    private float _currentScissorsBulletsCooldown = 2.5f;                           // Time before next bullet can be shot
    private bool _canShootScissorsBullets = true;

    private Queue<GameObject> _bulletPool = new Queue<GameObject>();            // Stores all common bullets available for shooting
    private Queue<GameObject> _scissorsBulletPool = new Queue<GameObject>();    // Stores all scissors bullets available for shooting
    private void Start()
    {
        _currentCommonBulletsCooldown = _shootCommonBulletsCooldown;
        _currentCommonBulletsCooldown = _shootScissorsBulletsCooldown;

        InitializePool(10, _commonBulletType); // Initialize the pool with 10 bullets
        InitializePool(10, _scissorsBulletType);

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
        if (Input.GetKeyDown(KeyCode.Mouse0) && _canShootCommonBullets == true)
        {
            shoot(_commonBulletType);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && _canShootScissorsBullets == true)
        {
            shoot(_scissorsBulletType);
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
        }
    }

    public void returnBulletToPool(GameObject bullet, string bulletType)
    {
        Debug.Log("Returning bullet to pool: " + bullet.name);

        Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
        if (rigidbody2D != null) rigidbody2D.velocity = Vector2.zero;

        bullet.SetActive(false);

        switch (bulletType)
        {
            case _commonBulletType :
                    _bulletPool.Enqueue(bullet);
                Debug.Log("Bullet pool size: " + _bulletPool.Count);
                break;
            case _scissorsBulletType :
                _scissorsBulletPool.Enqueue(bullet);
                Debug.Log("Scissors bullet pool size: " + _scissorsBulletPool.Count);
                break;
        }
    }
    public float accessCooldown
    {
        get { return _shootCommonBulletsCooldown; }
        set { _shootCommonBulletsCooldown = value; }
    }
}
