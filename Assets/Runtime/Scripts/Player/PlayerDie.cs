using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    public GameObject deathPanel;
    [SerializeField] private List<DeathScreenStats> _deathScreenStatsList;
    [SerializeField] private SpawnerManager spawnerManager;

    // Tags
    private const string _boundsTag = "Bounds";

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
            ItitializeDeathScreenStats();

            spawnerManager.gameObject.SetActive(false);
            deathPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void ItitializeDeathScreenStats()
    {
        _deathScreenStatsList[0].SetText(KillCountManager.RockKillCount);
        _deathScreenStatsList[1].SetText(KillCountManager.PaperKillCount);
        _deathScreenStatsList[2].SetText(KillCountManager.ScissorsKillCount);
    }
}
