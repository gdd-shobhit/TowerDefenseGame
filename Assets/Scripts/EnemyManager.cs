using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns, manages, and deletes enemies
/// </summary>
public class EnemyManager : MonoBehaviour
{
    private List<EnemyInstance> enemies = new List<EnemyInstance>();
    private List<float> spawnTimers;
    private List<float> spawnRates;

    public GameObject enemySpawn;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimers = new List<float>() { 0, 0, 0 };
        spawnRates = new List<float> { 1f, 4.5f, 3.5f };
    }

    /// <summary>
    /// Spawns the enemies for each frame
    /// </summary>
    void Update()
    {
        if (GameManager.instance.inRound)
        {
            for(int i = 0; i < spawnTimers.Count; i++)
            {
                spawnTimers[i] += Time.deltaTime;
                while(spawnTimers[i] >= spawnRates[i])
                {
                    spawnTimers[i] -= spawnRates[i];
                    SpawnEnemy((EnemyType)i);
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
    }

    /// <summary>
    /// Destroys an enemy when they die
    /// </summary>
    public void DestroyEnemyInstance(EnemyInstance enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
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
