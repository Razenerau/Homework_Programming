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

    [SerializeField] protected readonly float _speed = 0f;                    //Speed at which the bullet moves  
    [SerializeField] protected readonly float _deathTime = 2f;                //How long before the bullet dies 
    
    [SerializeField] protected readonly string _targetEnemyTag;               //The enemy the bullet would kill
    [SerializeField] protected readonly string _lethalEnemyTag;               //The enemy that will kill the bullet
    [SerializeField] protected readonly string _neutralEnemyTag;              //The enemy with the same type as the bullet

    [SerializeField] protected BulletModel _bulletModel;

    //==================================================================================================================
    // Bullet Set Up  
    //==================================================================================================================

    BulletControllerBase()
    {
        gameObject.name = _bulletModel.Name;
        _targetEnemyTag = _bulletModel.TargetEnemyTag;
        _lethalEnemyTag = _bulletModel.LethalEnemyTag;
        _neutralEnemyTag = _bulletModel.NeutralEnemyTag;

        Debug.Log($"{name} was constructed");
    }

    //Waits till timer is out then destroys the bullet 
    private IEnumerator Expiration()
    {
        yield return new WaitForSeconds(_deathTime);
        //gameObject.SetActive(false);
        //ReturnBulletToPool(gameObject, _bulletType);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if(tag == _lethalEnemyTag)                  //Collision with object that kills the bullet
        {
            Debug.Log("collitsion with lethal enemy");
        }
        else if (tag == _targetEnemyTag)            //Collision with object that the bullet defeats
        {
            Debug.Log("collitsion with target enemy");
        }
        else if (tag == _neutralEnemyTag)           //Collision with the same object as the bullet
        {
            RepellSelf(collision.gameObject);
            Debug.Log("collitsion with neutral enemy");
        }
        else                                                    //Collision with bounds or other objects
        {

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
        Vector2 normal = collisionObj.GetComponent<Rigidbody2D>().linearVelocity.normalized;
        Vector2 incidentDirection = _rigidbody2D.linearVelocity.normalized;
        Vector2 reflectedDirection = Vector2.Reflect(incidentDirection, normal);
        _rigidbody2D.linearVelocity = reflectedDirection * _rigidbody2D.linearVelocity.magnitude;
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
        _rigidbody2D.linearVelocity = velocity;
    }

    protected GameObject TakeBulletFromPool(Queue<GameObject> pool)
    {
        GameObject bullet = pool.Dequeue();
        bullet.SetActive(true);
        return bullet;
    }

    protected void ReturnToBulletPool(Queue<GameObject> pool)
    {
        gameObject.SetActive(false);
        pool.Enqueue(gameObject);
    }
}
