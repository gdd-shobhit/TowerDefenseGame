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
        if (counter >= Registry.path.Count)
        {
            //gameObject.SetActive(false);

            //Not supposed to but i dont know about how many enemies and how we gonna do
            //so just destyroying it right now
            //GameObject.Destroy(gameObject);

            transform.position = new Vector3(0, 0, -3);
            counter = 0;
        }

        if (counter < Registry.path.Count)
        {
            transform.position += (Registry.path[counter].transform.position - transform.position).normalized * Time.deltaTime * speed;
            if ((Registry.path[counter].transform.position - transform.position).magnitude < 1f)
            {
                transform.position = Registry.path[counter].transform.position;
                counter++;
            }
        }
    }
}
