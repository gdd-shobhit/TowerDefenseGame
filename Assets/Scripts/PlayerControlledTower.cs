using UnityEngine;

/// <summary>
/// Allows a tower to be moved by the player
/// </summary>
public class PlayerControlledTower : MonoBehaviour
{
    /// <summary>
    /// Controls player tower movement
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (transform.position.y < -0.1f)
                transform.Translate(new Vector3(0, 0, -5));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (transform.position.y > -44.9f)
                transform.Translate(new Vector3(0, 0, 5));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (transform.position.x > 0.1f)
                transform.Translate(new Vector3(-5, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (transform.position.x < 44.9f)
                transform.Translate(new Vector3(5, 0, 0));
        }
    }
}
