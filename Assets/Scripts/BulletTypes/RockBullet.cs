using System.Collections;
using UnityEngine;

//Controls the bullet that can hit player on Enemy 
public class Bullet : MonoBehaviour
{
    //==================================================================================================================
    // Variables 
    //==================================================================================================================

    //Used by Player Spawner 
    private Camera _camera;    //Camera Game Object 
    private Vector3 _mousePos; //Current Mouse Position  

    //Movement Controls 
    private Rigidbody2D _rigidbody2D; //The rigidbody that will move the bullet 
    public float speed = 2.5f;           //Speed at which the bullet moves 

    //Flag and Timer 
    public float deathTime = 1.2f;   //How long before the bullet dies 
    public bool playerBullet = true; //Is the bullet used by player or enemy 

    // Bullet Types
    private const string _commonBulletType = "common";

    // Tags
    private const string _enemyRockBulletTag = "Enemy Rock";
    private const string _boundsTag = "Bounds";

    [SerializeField] private PlayerShoot _playerShoot;

    //==================================================================================================================
    // Bullet Set Up  
    //==================================================================================================================

    //If the player is shooting connect the camera and mousePos then set up bullet rotation 
    private void PlayerBullet()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _camera = GameObject.Find("Game_Camera").GetComponent<Camera>();
        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        RotationUpdate(_mousePos);
    }

    //Takes in the position of the mouse or the player, then calculates the rotation
    //And set it 
    private void RotationUpdate(Vector3 pos1)
    {
        var pos2 = transform.position;
        var dir = pos1 - pos2;
        var rotation = pos2 - pos1;
        _rigidbody2D.velocity = new Vector2(dir.x, dir.y).normalized * speed;
        var rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    //Waits till timer is out then destroys the bullet 
    private IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTime);
        gameObject.SetActive(false);
        _playerShoot.returnBulletToPool(gameObject, _commonBulletType);
    }

    public void SetPlayerShoot(PlayerShoot playerShoot)
    {
        _playerShoot = playerShoot;
        PlayerBullet();
        StartCoroutine(Death());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Bullet Collides with object\nObject's name: " + collision.gameObject.name

        //Collision with rock
        if(collision.gameObject.tag == _enemyRockBulletTag)
        {
            Vector2 normal = collision.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
            Vector2 incidentDirection = _rigidbody2D.velocity.normalized;
            Vector2 reflectedDirection = Vector2.Reflect(incidentDirection, normal);
            _rigidbody2D.velocity = reflectedDirection * _rigidbody2D.velocity.magnitude;

            Vector3 moveDirection = _rigidbody2D.velocity;
            transform.LookAt(transform.position + moveDirection);
        }
        else if (collision.gameObject.tag.Contains("Enemy") || collision.gameObject.tag == _boundsTag)
        {
            _playerShoot.returnBulletToPool(gameObject, _commonBulletType);
        }
    }
}