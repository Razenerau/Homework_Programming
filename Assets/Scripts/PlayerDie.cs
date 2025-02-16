using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    public GameObject deathPanel;
    private string _enemyTag = "Enemy";

    private void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_playerHealth == null) Debug.Log("Error. Health variable not found.");  

        if (collision.gameObject.tag == _enemyTag && _playerHealth.getHealth() != 1)
        {
            Destroy(collision.gameObject);
            _playerHealth.decreaseHealth(1);
        }
        else // If player's health reaches 0
        {
            deathPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
