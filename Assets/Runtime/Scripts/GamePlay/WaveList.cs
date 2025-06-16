using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveList : MonoBehaviour
{
    public static WaveList Instance;
    public List<Wave> WavesList;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}

[System.Serializable]
public class Wave
{
    public SpawnerManager.WaveType Type;
    public float Duration;
    public int MaxEnemyCount;
    public List<Requirement> Requirements;
}

[System.Serializable]
public class Requirement 
{
    public SpawnerManager.WaveType Type { get; private set; }
    public int Quantity { get; private set; }
}
