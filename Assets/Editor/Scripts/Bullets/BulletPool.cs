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
        InitializePools();
    }

    private void InitializePools()
    {

        foreach (GameObject bulletPrototype in _bulletPrototypes)
        {
            BulletControllerBase bulletController = bulletPrototype.GetComponent<BulletControllerBase>();
            int poolBulletNumber = bulletController.GetPoolBulletNumber();
            Queue<GameObject> pool = new Queue<GameObject>();
            Debug.Log($"Initialized {bulletPrototype.name} pool with {poolBulletNumber} bullets");

            for (int i = 0; i < poolBulletNumber; i++)
            {
                GameObject bullet = Instantiate(bulletPrototype);
                AddBulletToPool(bullet, pool);
            }

            _bulletPools.Add(bulletPrototype.name, pool);
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

    public Transform GetPoolTranform(GameObject bullet)
    {
        string parentName = bullet.name + " Bullet Pool";
        Transform existingParent = FindPoolParent(parentName);

        if (existingParent != null)
        {
            return existingParent;
        }
        else
        {
            return CreateNewPoolParent(parentName);
        }
    }

    private Transform FindPoolParent(string poolName)
    {
        foreach (Transform child in transform) // Only check direct children
        {
            if (child.name == poolName)
            {
                return child;
            }
        }
        return null;
    }
    private Transform CreateNewPoolParent(string poolName)
    {
        GameObject newParent = new GameObject(poolName);
        newParent.transform.SetParent(this.transform); // Parent to this object
        return newParent.transform;
        //bullet.transform.SetParent(newParent.transform);
    }
}
