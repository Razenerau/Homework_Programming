using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Dictionary<string, Queue<GameObject>> _bulletPools; // = new Dictionary<string, Queue<GameObject>>();

    protected GameObject TakeBulletFromPool(Queue<GameObject> pool)
    {
        GameObject bullet = pool.Dequeue();
        bullet.SetActive(true);
        return bullet;
    }

    protected void ReturnBulletToPool(Queue<GameObject> pool)
    {
        gameObject.SetActive(false);
        pool.Enqueue(gameObject);
    }
}
