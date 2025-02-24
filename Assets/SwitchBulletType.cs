using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBulletType : MonoBehaviour
{
    private string _currentBulletType;

    public string currentBulletType
    {
        get { return _currentBulletType; }
        private set { _currentBulletType = value; }
    }

    void Update()
    {
        switchBullet();
    }

    public void switchBullet()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

        }
    }
}
