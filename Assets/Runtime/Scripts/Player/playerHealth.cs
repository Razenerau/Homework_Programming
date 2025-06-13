using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public LivesCounter livesCounter;
    [SerializeField] private AudioModel audioModel;

    private int _health = 5;
    private int _maxHealth = 5;

    public int Health
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
            livesCounter.updateLivesCounter();
            UpdateMusic(1);
        }
    }

    public void DecreaseHealth(int damage)
    {
        Health -= damage;
        UpdateMusic(-1);
        livesCounter.updateLivesCounter();
    }

    public void IncreaseLife(int num)
    {
        Health += num;
        UpdateMusic(1);
        livesCounter.updateLivesCounter();
    }

    public int GetHealth() { return Health; }

    private void UpdateMusic(int num)
    {
        if (audioModel != null)
        {
            int health = GetHealth();

            if (health > 3)
            {
                audioModel.SetClip(5);
            }
            else if (health > 1)
            {
                audioModel.SetClip(3);
            }
            else
            {
                audioModel.SetClip(1);
            }

            
        }
        
    }
}
