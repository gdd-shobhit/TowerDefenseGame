using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Simple singelton
    public static GameManager instance;
    public GameObject player;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            player = GameObject.Find("Player");
            Registry.LoadRegistry();
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {

    }
}
