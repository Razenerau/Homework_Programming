using System.Threading;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject preFab;
    public Transform bulletTrash;
    public Transform bulletSpawn;

    private bool _canShoot = true;

    private const float Timer = 0.5f; // Interval between bullets
    private float _currentTime = 0.5f; // Time before next bullet can be shot

    private void Update()
    {
        if (!_canShoot)
        {
            _currentTime -= Time.deltaTime;

            if(_currentTime < 0)
            {
                _canShoot = true;
                _currentTime = Timer;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && _canShoot == true)
        {
            GameObject bullet = Instantiate(preFab, bulletSpawn.position, Quaternion.identity);

            bullet.transform.SetParent(bulletTrash);

            _canShoot = false;
        }

    }
}
