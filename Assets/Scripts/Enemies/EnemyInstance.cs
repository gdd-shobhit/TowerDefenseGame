using UnityEngine;

/// <summary>
/// An instance of an enemy on the map
/// </summary>
public class EnemyInstance : MonoBehaviour
{
    [SerializeField]
    private int health;

    /// <summary>
    /// Deals damage to the enemy instance
    /// </summary>
    /// <param name="damage">Amount of damage to take</param>
    /// <returns>Whether this kills the instance or not</returns>
    public bool TakeDamage(int damage)
    {
        health -= damage;
        return health <= 0;
    }
}
