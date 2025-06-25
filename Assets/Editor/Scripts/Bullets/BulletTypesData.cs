using System.Collections.Generic;
using UnityEngine;

public class BulletTypesData : MonoBehaviour
{
    [SerializeField] private List<GameObject> _currentBulletTypes;

    private string GetBulletName(int index)
    {
        BulletControllerBase bulletConrollerBase = _currentBulletTypes[index].GetComponent<BulletControllerBase>();
        BulletModel bulletModel = bulletConrollerBase.GetBulletModel();
        return bulletModel.Name;
    }
}