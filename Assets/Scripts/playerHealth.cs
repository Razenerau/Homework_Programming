using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int _health = 5;
    
    public void decreaseHealth(int damage) { _health -= damage; }

    public int getHealth() { return _health; }
}
