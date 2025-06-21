using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    [SerializeField] private List<GameObject> _bulletPrototypes;
    [SerializeField] private Dictionary<string, Queue<GameObject>> _bulletPools;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _bulletPools = new Dictionary<string, Queue<GameObject>>();
        //InitializePool();
    }

    private void InitialzePool()
    {
        foreach (GameObject bulletPrototype in _bulletPrototypes)
        {
            BulletControllerBase bulletController = bulletPrototype.GetComponent<BulletControllerBase>();
            int poolBulletNumber = bulletController.GetPoolBulletNumber();
            Queue<GameObject> pool = new Queue<GameObject>();

            for (int i = 0; i < poolBulletNumber; i++)
            {
                GameObject bullet = Instantiate(bulletPrototype);
                AddBulletToPool(bullet, pool);
            }
        }
    }

    public GameObject GetBulletFromPool(Queue<GameObject> pool)
    {
        GameObject bullet = pool.Dequeue();
        bullet.SetActive(true);
        return bullet;
    }

    public void AddBulletToPool(GameObject bullet, Queue<GameObject> pool)
    {
        bullet.SetActive(false);
        pool.Enqueue(bullet);
    }
}
