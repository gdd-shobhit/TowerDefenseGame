using UnityEngine;

/// <summary>
/// An instance of an enemy on the map
/// </summary>
public class EnemyInstance : MonoBehaviour
{
    private int health;
    private float distanceTravelled;
    private int enemyType = -1;

    /// <summary>
    /// Returns the distance this enemy has travelled
    /// </summary>
    public float GetDistanceTravelled()
    {
        return distanceTravelled;
    }

    /// <summary>
    /// Adds to the distance this enemy has travelled
    /// </summary>
    /// <param name="distance">Distance it's travelled</param>
    public void Travel(float distance)
    {
        distanceTravelled += Mathf.Abs(distance);
    }

    /// <summary>
    /// Sets the enemy's health
    /// </summary>
    /// <param name="health">Starting health</param>
    public void SetHealth(int health)
    {
        this.health = health;
    }

    /// <summary>
    /// Deals damage to the enemy instance and destroys when it drops to 0
    /// </summary>
    /// <param name="damage">Amount of damage to take</param>
    /// <returns>Whether this kills the instance or not</returns>
    public bool TakeDamage(int damage)
    {
        health -= damage;
        return health <= 0;
    }

    /// <summary>
    /// sets the type of enemy that this instance is
    /// </summary>
    /// <param name="vType"></param>
    public void SetEnemyType(int vType)
    {
        enemyType = vType;
    }

    /// <summary>
    /// returns the type that the enemy is
    /// </summary>
    /// <returns></returns>
    public int GetEnemyType()
    {
        return enemyType;
    }

    /// <summary>
    /// Checks for if the enemy reached player or not
    /// </summary>
    private void Update()
    {       
        if (Vector3.Distance(gameObject.transform.position, Registry.path[Registry.path.Count - 1].transform.position) < 0.1)
        {
            // right now it doesnt decrease by 1 because there are instances where in few frames the magnitude is lesser than 0.1 alot of times
            // we can fix it when we say that this object dies or be inactive later
            GameManager.instance.player.GetComponent<Player>().TakeDamage();
            Debug.Log("NOOOOO");
            GameManager.instance.enemyManager.DestroyEnemyInstance(this);
        }
    }
}
