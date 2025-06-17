using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletModel", menuName = "Bullets/BulletModel")]
public class BulletModel : ScriptableObject
{
    public float _speed = 0f;                    //Speed at which the bullet moves  
    public float _deathTime = 2f;                //How long before the bullet dies 
    public Sprite _sprite;

    public string _targetEnemyTag;               //The enemy the bullet would kill
    public string _lethalEnemyTag;               //The enemy that will kill the bullet
    public string _neutralEnemyTag;              //The enemy with the same type as the bullet
}
