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
            BulletModel bulletModel = bulletController.GetBulletModel();
            Vector2 pos = GetPosition();
            Quaternion rotation = GetRotation();
            Vector2 direction = GetDirection();
            float speed = bulletModel.Speed;

            bulletController.Shoot(pos, direction * speed, rotation);
        } 
    }

    private Vector2 GetDirection()
    {
        float angleInDegrees = transform.eulerAngles.z + 90;
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
    }

    private Vector2 GetPosition() { return _spawnPoint.transform.position; }
    private Quaternion GetRotation() { return gameObject.transform.rotation; }
}
