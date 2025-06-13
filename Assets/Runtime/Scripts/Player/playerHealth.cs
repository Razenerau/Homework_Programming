using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public LivesCounter livesCounter;

    private int _health = 5;
    private int _maxHealth = 5;

    public int Health
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
            livesCounter.updateLivesCounter();
        }
    }

    public void DecreaseHealth(int damage)
    {
        Health -= damage;
        livesCounter.updateLivesCounter();
    }

    public void IncreaseLife(int num)
    {
        Health += num;
        livesCounter.updateLivesCounter();
    }

    public int GetHealth() { return Health; }
}
