using System.Collections;
using UnityEngine;

//Controls the bullet that can hit player on Enemy 
public class PaperBullet : MonoBehaviour
{
    //==================================================================================================================
    // Variables 
    //==================================================================================================================

    //Used by Player Spawner 
    private Camera _camera;    //Camera Game Object 
    private Vector3 _mousePos; //Current Mouse Position  

    //Movement Controls 
    private Rigidbody2D _rigidbody2D; //The rigidbody that will move the bullet 
    //public float speed = 0f;           //Speed at which the bullet moves 

    //Flag and Timer 
    public float deathTime = 2f;   //How long before the bullet dies 
    public bool playerBullet = true; //Is the bullet used by player or enemy 

    // Bullet Types and Tags
    private const string _bulletType = "paper";
    private const string _boundsTag = "Bounds";
    private const string _enemyPaperBulletTag = "Enemy Paper";
    private const string _enemyScissorsBulletTag = "Enemy Scissors";

    private const string _enemyTag = "Enemy";

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
        var rotation = pos2 - pos1;
        var rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    //Waits till timer is out then destroys the bullet 
    private IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTime);
        gameObject.SetActive(false);
        _playerShoot.returnBulletToPool(gameObject, _bulletType);
    }

    public void SetPlayerShoot(PlayerShoot playerShoot)
    {
        _playerShoot = playerShoot;
        PlayerBullet();
        StartCoroutine(Death());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Bullet Collides with object\nObject's name: " + collision.gameObject.name);

        //Collision with paper
        if (collision.gameObject.tag == _enemyPaperBulletTag)
        {
            Vector2 normal = collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity.normalized;
            Vector2 incidentDirection = _rigidbody2D.linearVelocity.normalized;
            Vector2 reflectedDirection = Vector2.Reflect(incidentDirection, normal);
            _rigidbody2D.linearVelocity = reflectedDirection * _rigidbody2D.linearVelocity.magnitude;
        }
        else if (collision.gameObject.tag == _enemyScissorsBulletTag)
        {
            _playerShoot.returnBulletToPool(gameObject, _bulletType);
        }
    }
}