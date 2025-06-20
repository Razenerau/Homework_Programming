using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class RockBulletConroller : BulletControllerBase
{
    private void Start()
    {
        gameObject.name = _bulletModel.Name;
        _targetEnemyTag = _bulletModel.TargetEnemyTag;
        _lethalEnemyTag = _bulletModel.LethalEnemyTag;
        _neutralEnemyTag = _bulletModel.NeutralEnemyTag;
        _deathTime = _bulletModel.DeathTime;


        Debug.Log($"{name} was constructed");
    }
}
