using UnityEngine;

public class PlayerShootBullet : MonoBehaviour
{
    [SerializeField] private BulletPool _bulletPool;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            GameObject bullet = _bulletPool.GetBulletFromPool("Rock");
            BulletControllerBase bulletController = bullet.GetComponent<BulletControllerBase>();
            Vector2 pos = new Vector2(4, 0);
            bulletController.Shoot(pos, Vector2.up * 3, Quaternion.identity);
        }
            
            
    }
}
