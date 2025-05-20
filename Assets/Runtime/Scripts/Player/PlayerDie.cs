using Unity.VisualScripting;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    public GameObject deathPanel;

    // Tags
    private const string _boundsTag = "Bounds";
    //private const string _playerRockBulletTag = "Player Rock Bullet";
    //private const string _playerPaperBulletTag = "Player Paper Bullet";
    //private const string _playerScissorsBulletTag = "Player Scissors Bullet";
    //private const string _enemyRockBulletTag = "Enemy Rock";
    //private const string _enemyPaperBulletTag = "Enemy Paper";
    //private const string _enemyrScissorsBulletTag = "Enemy Scissors";

    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_playerHealth == null) Debug.Log("Error. Health variable not found.");  

        if (collision.gameObject.tag.Contains("Enemy"))
        {
            Destroy(collision.gameObject);
            _playerHealth.decreaseHealth(1);
        }
        else if (collision.gameObject.tag == _boundsTag)
        {
            gameObject.transform.position = Vector2.zero;
            _playerHealth.decreaseHealth(1);
        }
        
        // If player's health reaches 0
        if (_playerHealth.getHealth() == 0)
        {
            deathPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
