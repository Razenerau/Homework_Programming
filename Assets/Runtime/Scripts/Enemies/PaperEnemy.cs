using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperEnemy : MonoBehaviour
{
    //==================================================================================================================
    // Variables 
    //==================================================================================================================

    //Movement Controls 
    new public Rigidbody2D rigidbody2D;       //The rigidbody that will move the bullet 
    public float minSpeed = 1f;           //Speed at which the bullet moves 
    public float maxSpeed = 4f;           //Speed at which the bullet moves 

    //Flag and Timer 
    public float deathTime = 100f;   //How long before the bullet dies 
    public bool playerBullet = true; //Is the bullet used by player or enemy 

    // Tags and Names 
    private const string _boundsTag = "Bounds";
    private const string _playerRockBulletTag = "Player Rock Bullet";
    private const string _playerPaperBulletTag = "Player Paper Bullet";
    private const string _playerScissorsBulletTag = "Player Scissors Bullet";
    private const string _gameControllerComponent = "GameController";

    // Component 
    private GameController _gameController;
    private PlayerShoot _playerShoot;
    public AudioSource deathSound;
    //public AudioClip clip;

    //==================================================================================================================
    // Base Method  
    //==================================================================================================================

    //Checks who is shooting the bullet and set up the bullet settings 
    private void Start()
    {
        _gameController = GameObject.Find(_gameControllerComponent).GetComponent<GameController>();
        StartCoroutine(Death());
    }

    public void SetSpeed(Vector3 newSpeed)
    {
        rigidbody2D.velocity = newSpeed * Random.Range(minSpeed,maxSpeed);
    }

    //==================================================================================================================
    // Bullet Set Up  
    //==================================================================================================================

    //Waits till timer is out then destroys the bullet 
    private IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If it touches the scissors, it destroys 
        if (collision.gameObject.tag == _playerScissorsBulletTag)
        {
            //Updates the Score 
            _gameController.UpdateScore(2);
            KillCountManager.Incerement(Structs.BulletType.PAPER);

            deathSound.Play();

            //spawn a heart
            DropHeartModel heartModel = GetComponent<DropHeartModel>();
            heartModel.CreateHeart();

            //Destorys the enemy 
            Destroy(gameObject);
        }
        // If it touches the paper, it bounces  
        if (collision.gameObject.tag == _playerPaperBulletTag)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x * -1, rigidbody2D.velocity.y * -1);
        }
        // If the enemy touches a bound it gets destored 
        else if(collision.gameObject.tag == _boundsTag)
        {
            Destroy(gameObject);
        }
    }
    public void SetPlayerShoot(PlayerShoot playerShoot)
    { 
        if (playerShoot != null)
        {
            _playerShoot = playerShoot;
        }
        else
        {
            Debug.LogWarning("playerShoot not found");
        }
    }
}
