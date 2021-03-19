using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tower type names for readability
/// </summary>
public enum TowerType
{
    Basic,
    Bomb
}

/// <summary>
/// Enemy type names for readability
/// </summary>
public enum EnemyType
{
    Basic,
    Speedy,
    Tank
}

/// <summary>
/// Contains the templates for each tower type and can make instances from them
/// </summary>
public class Registry
{
    public static Dictionary<TowerType, TowerDefinition> towerDefinitions = new Dictionary<TowerType, TowerDefinition>()
    {
        {
            TowerType.Basic,
            new TowerDefinition
            (
                name: "Basic Tower",
                cost: 1,
                range: 2,
                targettingType: TargettingType.Single,
                minFireRate: 1,
                maxFireRate: 1,
                minDamage: 2,
                maxDamage: 2
            )
        },
        {
            TowerType.Bomb,
            new TowerDefinition
            (
                name: "Bomb Tower",
                cost: 3,
                range: 2,
                targettingType: TargettingType.Single,
                minFireRate: -2,
                maxFireRate: -2,
                minDamage: 2,
                maxDamage: 2,
                towerEffects: new List<TowerEffectDef>()
                {
                    new TowerEffectDef(TowerEffectType.ExplosiveShot, 1)
                }
            )
        }
    };

    public static Dictionary<EnemyType, EnemyDefinition> enemyDefinitions = new Dictionary<EnemyType, EnemyDefinition>()
    {
        {
            EnemyType.Basic,
            new EnemyDefinition
            (
                name: "Basic Enemy",
                health: 2,
                moveSpeed: 1
            )
        },
        {
            EnemyType.Speedy,
            new EnemyDefinition
            (
                name: "Speedy Enemy",
                health: 2,
                moveSpeed: 2
            )
        },
        {
            EnemyType.Tank,
            new EnemyDefinition
            (
                name: "Tank Enemy",
                health: 4,
                moveSpeed: 1
            )
        }
    };

    public static List<GameObject> path = new List<GameObject>();

    /// <summary>
    /// Loads any registry resources that are not hard-coded
    /// </summary>
    public static void LoadRegistry()
    {
        path.AddRange(GameObject.FindGameObjectsWithTag("Path"));
        path.Sort(delegate (GameObject g1, GameObject g2) { return g1.name.CompareTo(g2.name); });
    }

    /// <summary>
    /// Generates a tower instance from a tower template
    /// </summary>
    /// <param name="towerType">The tower template to use</param>
    /// <returns>A new tower instance of the given type</returns>
    public static GameObject GenerateInstance(TowerType towerType, bool active = true)
    {
        return towerDefinitions[towerType].GenerateInstance(active);
    }

    /// <summary>
    /// Generates an enemy instance from an enemy template
    /// </summary>
    /// <param name="enemyType">The enemy template to use</param>
    /// <returns>A new enemy instance of the given type</returns>
    public static GameObject GenerateInstance(EnemyType enemyType)
    {
        GameObject enemy = enemyDefinitions[enemyType].GenerateInstance();
        enemy.GetComponent<PathFinder>().speed = enemyDefinitions[enemyType].moveSpeed;
        return enemy;
    }
}
