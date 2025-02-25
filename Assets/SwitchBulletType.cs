using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBulletType : MonoBehaviour
{
    private static string _currentBulletType = Structs.BulletType.ROCK;

    public static string currentBulletType
    {
        get { return _currentBulletType; }
    }

    void Update()
    {
        switchBullet();
    }

    public static void switchBullet()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentBulletType)
            {
                case Structs.BulletType.ROCK:
                    _currentBulletType = Structs.BulletType.PAPER;
                    break;
                case Structs.BulletType.PAPER:
                    _currentBulletType = Structs.BulletType.SCISSORS;
                    break;
                case Structs.BulletType.SCISSORS:
                    _currentBulletType = Structs.BulletType.ROCK;
                    break;
            }
            Debug.Log("Current bullet type: " +  _currentBulletType);
        }
    }
}
