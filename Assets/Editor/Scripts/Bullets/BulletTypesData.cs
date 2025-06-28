using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class BulletTypesData : MonoBehaviour
{
    [SerializeField] private List<GameObject> _currentBulletTypes;

    private string GetBulletName(int index)
    {
        List<GameObject> prototypesList = BulletPool.Instance.GetBulletPrototypesList();
        List<string> bulletNamesList = new List<string>();
        foreach (GameObject prototype in prototypesList)
        {
            BulletControllerBase bulletConrollerBase = _currentBulletTypes[index].GetComponent<BulletControllerBase>();
            BulletModel bulletModel = bulletConrollerBase.GetBulletModel();
            bulletNamesList.Add(bulletModel.Name);
        }
        //BulletControllerBase bulletConrollerBase = _currentBulletTypes[index].GetComponent<BulletControllerBase>();
        //BulletModel bulletModel = bulletConrollerBase.GetBulletModel();
        return bulletNamesList;
    }
}