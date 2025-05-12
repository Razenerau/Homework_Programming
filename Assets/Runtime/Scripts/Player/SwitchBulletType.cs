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
            //Pass ref to cooldownModel here

            //CooldownModel.SetStateColor(false);

            switch (currentBulletType)
            {
                case Structs.BulletType.ROCK:
                    _currentBulletType = Structs.BulletType.PAPER;
                    CooldownModel.SetState(CooldownModel.States.PAPER);
                    break;
                case Structs.BulletType.PAPER:
                    _currentBulletType = Structs.BulletType.SCISSORS;
                    CooldownModel.SetState(CooldownModel.States.SCISSORS);
                    break;
                case Structs.BulletType.SCISSORS:
                    _currentBulletType = Structs.BulletType.ROCK;
                    CooldownModel.SetState(CooldownModel.States.ROCK);
                    break;
            }
           //Debug.Log("Current bullet type: " +  _currentBulletType);
        }
    }
}
