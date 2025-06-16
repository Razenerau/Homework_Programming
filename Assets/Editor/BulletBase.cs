using Codice.ThemeImages;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
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
    private IEnumerator Death()
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
            case _neutralEnemyTag:       //Repell each-other
                Repell(collision.gameObject);
                break;
            default:                     //Destroy this

                break;
        }

        /*if (collision.gameObject.tag == _enemyPaperBulletTag)
        {
            Vector2 normal = collision.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
            Vector2 incidentDirection = _rigidbody2D.velocity.normalized;
            Vector2 reflectedDirection = Vector2.Reflect(incidentDirection, normal);
            _rigidbody2D.velocity = reflectedDirection * _rigidbody2D.velocity.magnitude;
        }
        else if (collision.gameObject.tag == _enemyScissorsBulletTag)
        {
            _playerShoot.returnBulletToPool(gameObject, _bulletType);
        }*/
    }

    protected void Repell(GameObject collisionObj)
    {
        Rigidbody2D _rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        Vector2 normal = collisionObj.GetComponent<Rigidbody2D>().velocity.normalized;
        Vector2 incidentDirection = _rigidbody2D.velocity.normalized;
        Vector2 reflectedDirection = Vector2.Reflect(incidentDirection, normal);
        _rigidbody2D.velocity = reflectedDirection * _rigidbody2D.velocity.magnitude;
    }
}
