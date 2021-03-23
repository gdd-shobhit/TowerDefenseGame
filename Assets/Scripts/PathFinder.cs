using UnityEngine;

/// <summary>
/// Moves a gameobject smoothly along a stored path over time
/// </summary>
public class PathFinder : MonoBehaviour
{
    public float speed = 1.0f;

    int counter = 0;

    /// <summary>
    /// Moves the pathfinder to the next spot on the path
    /// </summary>
    void FixedUpdate()
    {
        if (counter < Registry.path.Count)
        {
            EnemyInstance thisEnemy = GetComponent<EnemyInstance>();
            Vector3 distanceTravelled = (Registry.path[counter].transform.position - transform.position).normalized * Time.deltaTime * speed;
            transform.position += distanceTravelled;
            thisEnemy?.Travel(distanceTravelled.magnitude);
            if (Vector3.Distance(Registry.path[counter].transform.position, transform.position) < 1f)
            {
                thisEnemy?.Travel(Vector3.Distance(Registry.path[counter].transform.position, transform.position));
                transform.position = Registry.path[counter].transform.position;
                counter++;
            }
        }
    }
}
