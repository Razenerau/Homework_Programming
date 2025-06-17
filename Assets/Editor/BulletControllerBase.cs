using Codice.ThemeImages;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

public abstract class BulletControllerBase : MonoBehaviour
{
    //==================================================================================================================
    // Variables 
    //================================================================================================================== 

    //Movement Controls 
   
    [SerializeField] protected float _speed = 0f;                    //Speed at which the bullet moves  
    [SerializeField] protected float _deathTime = 2f;                //How long before the bullet dies 
    [SerializeField] protected Sprite _sprite;
    
    [SerializeField] protected UnityEngine.Tag _targetEnemyTag;            //The enemy the bullet would kill
    [SerializeField] protected UnityEngine.Tag _lethalEnemyTag;         //The enemy that will kill the bullet
    [SerializeField] protected UnityEngine.Tag _neutralEnemyTag;        //The enemy with the same type as the bullet

    //==================================================================================================================
    // Bullet Set Up  
    //==================================================================================================================

    //Waits till timer is out then destroys the bullet 
    private IEnumerator Expiration()
    {
        yield return new WaitForSeconds(_deathTime);
        //gameObject.SetActive(false);
        //ReturnBulletToPool(gameObject, _bulletType);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collision with neutral enemy

        switch (collision.gameObject.tag)
        {
            case _lethalEnemyTag:        //Destroy this

                break;
            case _targetEnemyTag:        //Destroy enemy

                break;
            case _neutralEnemyTag:       //RepellSelf each-other
                RepellSelf(collision.gameObject);
                break;
            default:                     //Destroy this

                break;
        }
    }

    protected void Shoot(Vector2 pos, Vector2 velocity, Quaternion rotation)
    {
        SetPosition(pos);
        SetVelocity(velocity);
        SetRotation(rotation);
    }

    protected void RepellSelf(GameObject collisionObj)
    {
        Rigidbody2D _rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        Vector2 normal = collisionObj.GetComponent<Rigidbody2D>().velocity.normalized;
        Vector2 incidentDirection = _rigidbody2D.velocity.normalized;
        Vector2 reflectedDirection = Vector2.Reflect(incidentDirection, normal);
        _rigidbody2D.velocity = reflectedDirection * _rigidbody2D.velocity.magnitude;
    }

    //--------------------------------------------------------------------------------
    //                  GETTER AND SETTER
    //--------------------------------------------------------------------------------

    protected void SetPosition(Vector2 pos)
    {
        gameObject.transform.position = pos;
    }

    protected void SetRotation(Quaternion rotation)
    {
        gameObject.transform.rotation = rotation;
    }

    protected void SetVelocity(Vector2 velocity)
    {
        Rigidbody2D _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = velocity;
    }

    protected void ReturnToBulletPool(GameObject[] pool)
    {

    }
}
