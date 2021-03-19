using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Simple singelton
    public static GameManager instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
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
