using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class BulletTypesData : MonoBehaviour
{
    // Stores bullet types available for player to use by pressing RMS or SPACE
    [SerializeField] private List<GameObject> _currentBulletTypes;
    public static BulletTypesData Instance { get; private set; }

    // Returns a list of names of bullets from the list selected by the game;
    private List<string> GetBulletNames()
    {
        List<GameObject> prototypesList = BulletPool.Instance.GetBulletPrototypesList();
        List<string> bulletNamesList = new List<string>();

        foreach (GameObject bulletType in _currentBulletTypes)
        {
            BulletControllerBase bulletConrollerBase = bulletType.GetComponent<BulletControllerBase>();
            BulletModel bulletModel = bulletConrollerBase.GetBulletModel();
            bulletNamesList.Add(bulletModel.Name);
        }
        return bulletNamesList;
    }

    public void InitializeBulletData()
    {
        Debug.Log(Instance.GetBulletNames());
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}