using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns, manages, and deletes enemies
/// </summary>
public class EnemyManager : MonoBehaviour
{
    private List<EnemyInstance> enemies = new List<EnemyInstance>();
    private float spawnTimers;
    private float spawnRates;
    private int round;
    private int numberOfEnemyTypes;
    private List<int> amountOfEnemyTypes = new List<int>();
    private List<int> enemiesDestroyed = new List<int>();
    private List<int> enemiesSpawnedIn = new List<int>();

    public GameObject enemySpawn;

    // Start is called before the first frame update
    void Start()
    {
        round = 1;
        amountOfEnemyTypes.Add(5 * round + 1);
        enemiesDestroyed.Add(0);
        enemiesSpawnedIn.Add(0);
        numberOfEnemyTypes = 3;

        for (int i = 1; i < numberOfEnemyTypes; i++)
        {
            amountOfEnemyTypes.Add(0);
            enemiesDestroyed.Add(0);
            enemiesSpawnedIn.Add(0);
        }

        spawnTimers = 0;
        spawnRates = 2.5f;
    }

    /// <summary>
    /// Spawns the enemies for each frame
    /// </summary>
    void Update()
    {
        int finishedSpawns = 0;

        //checks to see if the maximum amount of 
        //enemies are spawned in per round.
        for (int i = 0; i < numberOfEnemyTypes; i++)
        { 
            if (amountOfEnemyTypes[i] == enemiesSpawnedIn[i])
            {
                finishedSpawns++;
            }
        }


        if (GameManager.instance.inRound)
        {   
            //makes sure that at least one enemy type can still be spawned in
            if (finishedSpawns < numberOfEnemyTypes)
            {
                
                spawnTimers += Time.deltaTime;
                while (spawnTimers >= spawnRates)
                {
                    spawnTimers = 0;
                
                    //makes sure the enemy type is not spawning in only enemies that
                    //still have enemies from their total amount to spawn in.
                    int enemyType = Random.Range(0, 3);
                    while (amountOfEnemyTypes[enemyType] == enemiesSpawnedIn[enemyType])
                    {
                        enemyType = Random.Range(0, 3);
                        Debug.Log("nooooo");
                    }
                
                    Debug.Log(enemyType + " = ememy type spawn");
                    enemiesSpawnedIn[enemyType]++;
                    SpawnEnemy((EnemyType)enemyType);
                
                }
            }
        }
    }

    /// <summary>
    /// Spawns an enemy of a given type at the enemy spawn point
    /// </summary>
    /// <param name="type">Type of enemy to spawn</param>
    private void SpawnEnemy(EnemyType type)
    {
        enemies.Add(Registry.GenerateInstance(type).GetComponent<EnemyInstance>());
        enemies[enemies.Count - 1].transform.position = enemySpawn.transform.position;
        enemies[enemies.Count - 1].SetEnemyType((int)type);
    }

    /// <summary>
    /// Destroys an enemy when they die
    /// </summary>
    public void DestroyEnemyInstance(EnemyInstance enemy)
    {
        enemiesDestroyed[enemy.GetEnemyType()]++;
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);

        int enemiesLeft = 0;
        for(int i = 0; i < numberOfEnemyTypes; i++)
        {
            if(enemiesDestroyed[i] == amountOfEnemyTypes[i])
            {
                Debug.Log(enemiesDestroyed[i] + " enemies Destroyed for " + i);
                enemiesLeft++;
            }
        }

        if(enemiesLeft == numberOfEnemyTypes)
        {

            //GameManager.instance.inRound = false;
            //GameManager.instance.StartRoundsButton.SetActive(true);
            spawnTimers = 0;
            spawnRates = 2.5f;
            IncreaseRound();
        } 
    }

    /// <summary>
    /// whenever a round is started increase the round amount
    /// reset the number of enemies that were spawned in, destroyed
    /// then recalculate the amount of enemies for this round
    /// </summary>
    public void IncreaseRound()
    {
        round++;
        amountOfEnemyTypes[0] = 5 * round + 1;
        enemiesDestroyed[0] = 0;
        enemiesSpawnedIn[0] = 0;


        for (int i = 1; i < numberOfEnemyTypes; i++)
        {
            enemiesDestroyed[i] = 0;
            enemiesSpawnedIn[i] = 0;

            if (round % (i + 1) == 0)
            {
                amountOfEnemyTypes[i] += 2 * round;
            }
        }
    }

    /// <summary>
    /// Gets all enemies within a range of a tower
    /// </summary>
    /// <param name="tower">The tower to search around</param>
    /// <returns>List of enemies in range</returns>
    public List<EnemyInstance> GetEnemiesAroundTower(TowerInstance tower)
    {
        return GetEnemiesAroundPosition(tower.transform.position, 2.5f + 5 * Registry.towerDefinitions[tower.type].range);
    }

    /// <summary>
    /// Gets all enemies within a range of a point in 2d space
    /// </summary>
    /// <param name="position">The position to search around</param>
    /// <param name="range">The range to search</param>
    /// <returns>List of enemies in range</returns>
    public List<EnemyInstance> GetEnemiesAroundPosition(Vector2 position, float range)
    {
        List<EnemyInstance> nearbyEnemies = new List<EnemyInstance>();
        foreach (EnemyInstance enemy in enemies)
        {
            Vector2 enemyPos = enemy.transform.position;
            if (enemyPos.x >= position.x - range && enemyPos.x <= position.x + range && enemyPos.y >= position.y - range && enemyPos.y <= position.y + range)
            {
                nearbyEnemies.Add(enemy);
            }
        }
        nearbyEnemies.Sort(delegate (EnemyInstance e1, EnemyInstance e2) { return Mathf.RoundToInt(Mathf.Sign(e2.GetDistanceTravelled() - e1.GetDistanceTravelled())); });
        return nearbyEnemies;
    }
}
