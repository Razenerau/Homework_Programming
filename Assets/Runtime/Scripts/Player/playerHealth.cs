using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public LivesCounter livesCounter;

    private int _health = 5;
    private int _maxHealth = 5;

    public int health
    {
        get => _health;
        private set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
        }
    }

    public void decreaseHealth(int damage)
    {
        health -= damage;
        livesCounter.updateLivesCounter();
    }

    public void increaseLife(int num)
    {
        health += num;
        livesCounter.updateLivesCounter();
    }

    public int GetHealth() { return health; }
}
