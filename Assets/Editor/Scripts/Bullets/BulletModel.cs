using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletModel", menuName = "Bullets/BulletModel")]
public class BulletModel : ScriptableObject
{
    public float Speed = 0f;                    //Speed at which the bullet moves  
    public float DeathTime = 2f;                //How long before the bullet dies 
    public Sprite Sprite;
    public float Area;

    public string TargetEnemyTag;               //The enemy the bullet would kill
    public string LethalEnemyTag;               //The enemy that will kill the bullet
    public string NeutralEnemyTag;              //The enemy with the same type as the bullet
}
