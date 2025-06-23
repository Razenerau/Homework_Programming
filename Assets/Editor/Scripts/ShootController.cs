using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private GameObject _spawnPoint;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            GameObject bullet = _bulletPool.GetBulletFromPool("Rock");

            if(bullet == null) {
                return;
            }

            BulletControllerBase bulletController = bullet.GetComponent<BulletControllerBase>();
            Vector2 pos = _spawnPoint.transform.position;
            Quaternion rotation = gameObject.transform.rotation;

            float angleInDegrees = transform.eulerAngles.z + 90;
            float angleInRadians = angleInDegrees * Mathf.Deg2Rad;

            Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

            bulletController.Shoot(pos, direction * 3, rotation);
        }
            
            
    }
}
