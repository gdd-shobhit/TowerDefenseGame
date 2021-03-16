using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    List<GameObject> paths;
    // right now its hardcoded. Its gonna be automated for final
    public GameObject path1;
    public GameObject path2;
    public GameObject path3;
    public GameObject path4;
    public GameObject path5;
    public GameObject path6;
    public GameObject path7;
    public float speed = 1.0f;

    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        paths = new List<GameObject>();
        paths.Add(path1);
        paths.Add(path2);
        paths.Add(path3);
        paths.Add(path4);
        paths.Add(path5);
        paths.Add(path6);
        paths.Add(path7);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
  
        if (counter >= paths.Count)
        {
            //gameObject.SetActive(false);

            //Not supposed to but i dont know about how many enemies and how we gonna do
            //so just destyroying it right now
            //GameObject.Destroy(gameObject);

            transform.position = new Vector3(0, 0, -3);
            counter = 0;
        }

        if (counter < paths.Count)
        {
            this.transform.position += (paths[counter].transform.position - transform.position).normalized * Time.deltaTime * speed;
            if ((paths[counter].transform.position - this.transform.position).magnitude < 1f)
            {
                counter++;
            }
        }

    }
}
