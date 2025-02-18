using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject preFab;
    public GameObject enemyPreFab;
    public GameObject Spawner;
    public Transform bulletTrash;
    public Transform bulletSpawn;

    private Enemy _enemy;
    private SpawnEnemy _spawnEnemy;

    [SerializeField] private float _shootCooldown = 0.5f;            // Interval between bullets
    private float _currentCooldown = 0.5f;                           // Time before next bullet can be shot
    private bool _canShoot = true;

    private Queue<GameObject> _bulletPool = new Queue<GameObject>(); // Stores all bullets available for shooting
    private void Start()
    {
        _currentCooldown = _shootCooldown;
        InitializePool(10); // Initialize the pool with 10 bullets

        _enemy = enemyPreFab.GetComponent<Enemy>();
        _enemy.SetPlayerShootEnemy(this);

        _spawnEnemy = Spawner.GetComponent<SpawnEnemy>();
        _spawnEnemy.SetPlayerShootSpawnEnemy(this);
    }

    private void Update()
    {
        // Time interval between shooting bullets
        handleBulletCoolDown();

        // Shooting 
        if (Input.GetKeyDown(KeyCode.Mouse0) && _canShoot == true)
        {
            shoot();
        }
    }

    private void InitializePool(int poolSize)
    {
        for(int i = 0;  i < poolSize; i++)
        {
            GameObject bullet = Instantiate(preFab); // Creates a new bullet
            bullet.transform.SetParent(bulletTrash);
            bullet.SetActive(false);
            _bulletPool.Enqueue(bullet); // Adds bullet to the pool
        }
    }

    private void handleBulletCoolDown()
    {
        if (!_canShoot)
        {
            _currentCooldown -= Time.deltaTime;

            if (_currentCooldown < 0)
            {
                _canShoot = true;
                _currentCooldown = _shootCooldown;
            }
        }
    }

    private void shoot()
    {
        //GameObject bullet = Instantiate(preFab, bulletSpawn.position, Quaternion.identity);
        if(_bulletPool.Count > 0)
        {
            GameObject bullet = _bulletPool.Dequeue();
            bullet.transform.position = bulletSpawn.position;
            bullet.SetActive(true);
            bullet.transform.SetParent(bulletTrash);

            // Pass the PlayerShoot reference to the bullet
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null) bulletScript.SetPlayerShoot(this); // Pass the reference

            _canShoot = false;
        }
        else
        {
            Debug.Log("Bullet Pool is Empty!");
        }
    }

    public void returnBulletToPool(GameObject bullet)
    {
        Debug.Log("Returning bullet to pool: " + bullet.name);

        Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
        if(rigidbody2D != null) rigidbody2D.velocity = Vector2.zero;

        bullet.SetActive(false);
        _bulletPool.Enqueue(bullet);

        Debug.Log("Bullet pool size: " + _bulletPool.Count);
    }
    public float accessCooldown
    {
        get { return _shootCooldown; }
        set { _shootCooldown = value; }
    }
}
