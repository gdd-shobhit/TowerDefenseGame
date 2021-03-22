using UnityEngine;

/// <summary>
/// An instance of an enemy on the map
/// </summary>
public class EnemyInstance : MonoBehaviour
{
    [SerializeField]
    private int health;

    /// <summary>
    /// Deals damage to the enemy instance and destroys when it drops to 0
    /// </summary>
    /// <param name="damage">Amount of damage to take</param>
    /// <returns>Whether this kills the instance or not</returns>
    public bool TakeDamage(int damage)
    {
        health -= damage;

        // need enemy pooling
        // after enemy pooling
        // Use this code : gameObject.SetActive(false);

        if(health <=0)
            GameObject.Destroy(this);

        return health <= 0;
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
        }
    }
}
