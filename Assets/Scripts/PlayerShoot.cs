using System.Threading;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject preFab;
    public Transform bulletTrash;
    public Transform bulletSpawn;

    private bool _canShoot = true;

    private float _shootCooldown = 0.5f;      // Interval between bullets
    private float _currentCooldown = 0.5f;    // Time before next bullet can be shot

    public float accessCooldown
    {
        get { return _shootCooldown; }
        set { _shootCooldown = value; }
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
        GameObject bullet = Instantiate(preFab, bulletSpawn.position, Quaternion.identity);

        bullet.transform.SetParent(bulletTrash);

        _canShoot = false;
    }
}
