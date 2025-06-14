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
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            switch (currentBulletType)
            {
                case Structs.BulletType.ROCK:
                    PlayerView.Instance.SetPaper();
                    _currentBulletType = Structs.BulletType.PAPER;
                    CooldownModel.SetState(CooldownModel.States.PAPER);
                    break;
                case Structs.BulletType.PAPER:
                    PlayerView.Instance.SetScissors();
                    _currentBulletType = Structs.BulletType.SCISSORS;
                    CooldownModel.SetState(CooldownModel.States.SCISSORS);
                    break;
                case Structs.BulletType.SCISSORS:
                    PlayerView.Instance.SetRock(); // Update sprite
                    _currentBulletType = Structs.BulletType.ROCK; // Update bullet type
                    CooldownModel.SetState(CooldownModel.States.ROCK); // Update cooldown visuals
                    break;
            }
        }
    }
}
