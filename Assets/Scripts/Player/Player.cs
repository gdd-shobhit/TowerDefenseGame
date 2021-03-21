using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    // for now
    public int health;

    // Use this for initialization
    void Start()
    {
        health = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            // will load the gameover scene
            SceneManager.LoadScene(3);
        }
    }

    public void TakeDamage()
    {
        health--;
    }
}
