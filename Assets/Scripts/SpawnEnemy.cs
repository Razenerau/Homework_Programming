using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // 
    public List<Transform> transforms = new List<Transform>();
    public Transform enemyTrash;
    public GameObject rockPreFab;
    public GameObject paperPreFab;
    public GameObject scissorsPreFab;
    private int index = 0;

    // Player Components
    private PlayerShoot _playerShoot;

    //Bullet Spawning Timers 
    public float Timer = 30f;                //How long should it take till player can next bullet 
    private float _currentTime = 0.5f;       //Counter to allow player to shoot 
    private bool _canSpawn = true;           //Tells us if we can shoot or not 

    // Update is called once per frame
    void Update()
    {
        EnemySpawnTimer();
        EnemySpawn();
    }


    //Checks if the player can shoot, if they can't counts down till they can again  
    private void EnemySpawnTimer()
    {
        //If player can shoot don't do anything else, count down 
        if (_canSpawn) return;
        _currentTime -= Time.deltaTime;
        //If timer is less than 0 allow player to shoot and reset the counter 
        if (!(_currentTime <= 0)) return;
        _currentTime = Timer;
        _canSpawn = true;
    }

    private void EnemySpawn()
    {
        if(enemyTrash.childCount > 10){ return;}

        //Randomizes the postion and enemy
        Vector3 newPosition = GetOrientation();
        int randomEnemy = Random.Range(1, 4);

        GameObject enemy;

        switch (randomEnemy)
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
            default: // Scissors
                enemy = Instantiate(scissorsPreFab, newPosition, Quaternion.identity);

                //Attach to trash 
                enemy.transform.SetParent(enemyTrash);
                enemy.GetComponent<ScissorsEnemy>().SetSpeed(GetSpeed());
                break;
        }

        //Wait to spawn next asteroid 
        _canSpawn = false;
        //
        index++;

        if (index == transforms.Count)
        {
            index = 0;
        }
    }

    // Figures out which direction the object is shooting from 
    private Vector3 GetOrientation()
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
    public void SetPlayerShoot(PlayerShoot playerShoot)
    {
        if (playerShoot != null)
        {
            _playerShoot = playerShoot;
            Debug.Log("PlayerShoot reference set for: " + gameObject.name);
        }
        else
        {
            Debug.LogWarning("playerShoot not found");
        }
    }
}
