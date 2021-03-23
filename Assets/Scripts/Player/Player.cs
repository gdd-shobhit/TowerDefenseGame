using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the loss condition for the game
/// </summary>
public class Player : MonoBehaviour
{
    // for now
    public int health;

    // Use this for initialization
    void Start()
    {
        health = 20;
    }

    /// <summary>
    /// take care of changing scenes when health drops zero
    /// </summary>
    void Update()
    {
        if (health <= 0)
        {
            // will load the gameover scene
            SceneManager.LoadScene(3);
        }
    }

    /// <summary>
    /// Decreases health by 1 (take damage by 1)
    /// </summary>
    public void TakeDamage()
    {
        health--;
    }
}
