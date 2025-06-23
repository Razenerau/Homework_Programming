using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletModel", menuName = "Bullets/BulletModel")]
public class BulletModel : ScriptableObject
{
    public string Name;

    [Header("Variables")]
    public float Area;                          //Area of the bullet's "explosion." If 0 = no area damage
    public float Speed = 0f;                    //Speed at which the bullet moves  
    public float DeathTime = 2f;                //How long before the bullet dies 
    public int PoolBulletNumber;
    public float size;

    [Header("Sprites")]
    public Sprite LineArt;
    public Sprite Body;
    public Sprite Shadows;

    [Header("Tags")]
    public string TargetEnemyTag;               //The enemy the bullet would kill
    public string LethalEnemyTag;               //The enemy that will kill the bullet
    public string NeutralEnemyTag;              //The enemy with the same type as the bullet
}
