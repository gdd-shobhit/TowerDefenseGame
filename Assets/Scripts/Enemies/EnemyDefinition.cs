using UnityEngine;
/// <summary>
/// The information about a single enemy type
/// </summary>
public class EnemyDefinition
{
    public string name;
    public int health;
    public float moveSpeed;
    private GameObject prefab;

    public EnemyDefinition(string name, int health, float moveSpeed)
    {
        this.name = name;
        this.health = health;
        this.moveSpeed = moveSpeed;
        prefab = Resources.Load<GameObject>("Prefabs/Enemies/" + name);
    }

    /// <summary>
    /// Generates an enemy instance from an enemy template
    /// </summary>
    /// <returns>A new enemy instance of the given type</returns>
    public GameObject GenerateInstance()
    {
        GameObject enemy = GameObject.Instantiate(prefab);
        return enemy;
    }
}
