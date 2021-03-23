using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITowerPlace : MonoBehaviour
{
    public GameObject towertype;

    public UIHandler towerAccessor;

    // Start is called before the first frame update
    void Start()
    {
        UIHandler towerAccessor = gameObject.GetComponent<UIHandler>();
    }

    public void SpawnTower()
    {
        towerAccessor.currentTower = towertype; 
        towerAccessor.allTowers.Add(towertype);
        towerAccessor.bought = true;
    }

}
