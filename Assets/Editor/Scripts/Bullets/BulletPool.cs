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
            BulletModel bulletModel = bulletController.GetBulletModel();
            string name = bulletModel.Name;
            int poolBulletNumber = bulletController.GetPoolBulletNumber();
            Queue<GameObject> pool = new Queue<GameObject>();
            Debug.Log($"Initialized {bulletPrototype.name} pool with {poolBulletNumber} bullets");

            for (int i = 0; i < poolBulletNumber; i++)
            {
                GameObject bullet = Instantiate(bulletPrototype);
                AddBulletToPool(bullet, pool);
                //Debug.Log($"Bullet's name is {bullet.name}");
            }

            _bulletPools.Add(name, pool);
            //Debug.Log($"Dictionary name is {name}");
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
    }

    //--------------------------------------------------------------------------------
    //                  GETTER AND SETTER
    //--------------------------------------------------------------------------------

    public GameObject GetBulletFromPool(string bulletName)
    {
        Queue<GameObject> pool = _bulletPools[bulletName];
        if (pool.Count > 0)
        {
            GameObject bullet = pool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            Debug.Log("Queue is empry");
            return null;
        }
        
    }

    public void AddBulletToPool(GameObject bullet, Queue<GameObject> pool)
    {
        bullet.SetActive(false);
        pool.Enqueue(bullet);
    }

    public void AddBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        Queue<GameObject> pool = _bulletPools[bullet.name];
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
}
