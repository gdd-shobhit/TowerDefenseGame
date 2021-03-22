using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public int Currency;
    public GameObject currentTower;
    public List<GameObject> allTowers = new List<GameObject>();

    public Text currencyText;

    Vector3 screenPoint;
    Vector3 mousePos;
    Vector3 origin = new Vector3(0,0,0);

    public bool bought = false;

    // Start is called before the first frame update
    void Start()
    {
        Currency = 100;
        screenPoint.z = 10.0f; //distance of the plane from the camera

    }

    // Update is called once per frame
    void Update()
    {
        //Display amount of currency
        currencyText.text = "Funds: " + Currency;

        //To select a placed tower
        mousePos = Input.mousePosition;
        Ray selectRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        //Set mouse location in respect to camera
        screenPoint = selectRay.origin;
        screenPoint.z = 10;


        if (Input.GetMouseButtonDown(0)) //On a mouse Click
        {
            if (bought)
            {
                Instantiate(currentTower, screenPoint, Quaternion.identity);
                currentTower = null;

                bought = false;
            }

            //To click a placed tower (can be used later to see the stats or range of placed towers)
            if (Physics.Raycast(selectRay, out hit))
            {
                Debug.Log(hit.collider.name);
            }
        }
    }

}
