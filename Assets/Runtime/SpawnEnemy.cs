using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // 
    public List<Transform> transforms = new List<Transform>();
    public Transform enemyTrash;
    public GameObject rockPreFab;
    public GameObject paperPreFab;
    public GameObject scissorsPreFab;
    public int maxEnemyCount;
    private int index = 0;

    /* Update is called once per frame
    void Update()
    {
        EnemySpawn();
    } */

    public void EnemySpawn(SpawnerManager.WaveType type)
    {
        if (enemyTrash.childCount > maxEnemyCount) { return; }

        Vector3 newPosition = GetRandomOrientation();
        int enemyType;

        switch (type)
        {
            case SpawnerManager.WaveType.ROCK:
                enemyType = 1;
                break;
            case SpawnerManager.WaveType.PAPER:
                enemyType = 2;
                break;
            case SpawnerManager.WaveType.SCISSORS:
                enemyType = 3;
                break;
            case SpawnerManager.WaveType.ROCK_PAPER:
                enemyType = Random.Range(1, 3);
                break;
            case SpawnerManager.WaveType.ROCK_SCISSORS:
                enemyType = (int) 1.5f * (Random.Range(1, 3));
                break;
            case SpawnerManager.WaveType.PAPER_SCISSORS:
                enemyType = Random.Range(2, 4);
                break;
            default:
                enemyType = Random.Range(1, 4);
                break;
        }

        GameObject enemy;

        switch (enemyType)
        {
            case 1: // Rock
                enemy = Instantiate(rockPreFab, newPosition, Quaternion.identity);

                //Attach to trash 
                enemy.transform.SetParent(enemyTrash);
                enemy.GetComponent<Enemy>().SetSpeed(GetSpeed());
                break;
            case 2: // Paper
                enemy = Instantiate(paperPreFab, newPosition, Quaternion.identity);

                //Attach to trash 
                enemy.transform.SetParent(enemyTrash);
                enemy.GetComponent<PaperEnemy>().SetSpeed(GetSpeed());
                break;
            case 3: // Scissors
                enemy = Instantiate(scissorsPreFab, newPosition, Quaternion.identity);

                //Attach to trash 
                enemy.transform.SetParent(enemyTrash);
                enemy.GetComponent<ScissorsEnemy>().SetSpeed(GetSpeed());
                break;
        }
        index++;

        if (index == transforms.Count)
        {
            index = 0;
        }

    }

    // Figures out which direction the object is shooting from 
    private Vector3 GetRandomOrientation()
    {
        Vector3 newPosition = Vector3.zero;
        switch (index)
        {
            case 0:
                {
                    newPosition = new Vector3(Random.Range(transforms[0].position.x, transforms[1].position.x), transforms[0].position.y, transforms[0].position.z);
                    break;
                }
            case 1:
                {
                    newPosition = new Vector3(transforms[1].position.x, Random.Range(transforms[1].position.y, transforms[2].position.y), transforms[1].position.z);
                    break;
                }
            case 2:
                {
                    newPosition = new Vector3(Random.Range(transforms[2].position.x, transforms[3].position.x), transforms[2].position.y, transforms[2].position.z);
                    break;
                }
            case 3:
                {
                    newPosition = new Vector3(transforms[3].position.x, Random.Range(transforms[3].position.y, transforms[0].position.y), transforms[3].position.z);
                    break;
                }
        }
        return newPosition;
    }

    // Sets the direction in which the enemy will go into 
    private Vector3 GetSpeed()
    {
        Vector3 newSpeed = Vector3.zero;
        switch (index)
        {
            case 0:
                {
                    newSpeed = Vector3.down;
                    break;
                }
            case 1:
                {
                    newSpeed = Vector3.left;
                    break;
                }
            case 2:
                {
                    newSpeed = Vector3.up;
                    break;
                }
            case 3:
                {
                    newSpeed = Vector3.right;
                    break;
                }
        }
        return newSpeed;
    }
}
