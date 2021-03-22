using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Simple singelton
    public static GameManager instance;
    public GameObject player;
    public Text currencyCountText;
    public TileManager tileManager;
    public GameObject TowerInfoDisplay;

    private TowerType selectedTowerType = TowerType.None;
    private int currency;
    private int currencyCount { get { return currency; } set { currency = value; currencyCountText.text = "Currency: " + currency; } }
    private List<TowerInstance> towerInstances = new List<TowerInstance>();
    private Tile selectedTile;

    /// <summary>
    /// Sets up the start of the game
    /// </summary>
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            player = GameObject.Find("Player");
            Registry.LoadRegistry();
            currencyCount = 10;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {

    }

    /// <summary>
    /// Registers when a tile is clicked on and executes actions based on that
    /// </summary>
    /// <param name="position">The tile that was clicked</param>
    public void TileClicked(Vector2Int position)
    {
        selectedTile = tileManager.tiles[position.x, position.y];
        //If that tile already has a tower on it
        if (selectedTile.IsTaken())
        {
            selectedTowerType = TowerType.None;
            DisplayTowerInfo(selectedTile.occupyingTower);
        }
        //If placing nothing or unable to place on the selected tile
        else if (selectedTowerType == TowerType.None || selectedTile.GetPathTile() || currencyCount < Registry.towerDefinitions[selectedTowerType].cost)
        {
            selectedTowerType = TowerType.None;
            DisplayTowerInfo(selectedTowerType);
        }
        //If placing a new tower
        else
        {
            currencyCount -= Registry.towerDefinitions[selectedTowerType].cost;
            TowerDefinition towerInfo = Registry.towerDefinitions[selectedTowerType];
            GameObject newTower = Registry.GenerateInstance(selectedTowerType);
            towerInstances.Add(newTower.GetComponent<TowerInstance>());
            newTower.transform.position = selectedTile.transform.position + new Vector3(0, 0, -3);
            selectedTile.occupyingTower = newTower.GetComponent<TowerInstance>();
        }
    }

    /// <summary>
    /// Displays the info on a selected tower type
    /// </summary>
    /// <param name="type">Tower type to display</param>
    public void DisplayTowerInfo(TowerType type)
    {
        Text[] textList = TowerInfoDisplay.GetComponentsInChildren<Text>();
        if (type == TowerType.None)
        {
            textList[0].text = "";
            textList[1].text = "";
            textList[2].text = "";
            textList[3].text = "";
            textList[4].text = "";
            return;
        }
        TowerDefinition towerInfo = Registry.towerDefinitions[type];
        textList[0].text = towerInfo.name;
        textList[1].text = "Cost: " + towerInfo.cost;
        textList[2].text = "Range: " + towerInfo.range;
        textList[3].text = "Fire Rate: " + (towerInfo.minFireRate == towerInfo.maxFireRate ? "" + towerInfo.maxFireRate : (towerInfo.minFireRate + " - " + towerInfo.maxFireRate));
        textList[4].text = "Damage: " + (towerInfo.minDamage == towerInfo.maxDamage ? "" + towerInfo.maxDamage : (towerInfo.minDamage + " - " + towerInfo.maxDamage));

    }

    /// <summary>
    /// Displays the info on a selected tower instance
    /// </summary>
    /// <param name="type">Tower instance to display</param>
    public void DisplayTowerInfo(TowerInstance tower)
    {
        TowerDefinition towerInfo = Registry.towerDefinitions[tower.type];
        Text[] textList = TowerInfoDisplay.GetComponentsInChildren<Text>();
        textList[0].text = towerInfo.name + " at " + -selectedTile.GetPosition().x + ", " + -selectedTile.GetPosition().y;
        textList[1].text = "Cost: " + towerInfo.cost;
        textList[2].text = "Range: " + towerInfo.range;
        textList[3].text = "Fire Rate: " + selectedTile.occupyingTower.currentFireRate;
        textList[4].text = "Damage: " + selectedTile.occupyingTower.currentDamage;
    }

    /// <summary>
    /// Chooses what tower to try to place when clicking on a space
    /// </summary>
    /// <param name="type">Integer representation of the tower type, converted back to TowerType enum in function</param>
    public void SelectTowerToPlace(int type)
    {
        TowerType towerType = (TowerType)type;
        //Don't allow it if they don't have enough funds
        if (currencyCount < Registry.towerDefinitions[towerType].cost || towerType == selectedTowerType)
            selectedTowerType = TowerType.None;
        else
            selectedTowerType = towerType;
        DisplayTowerInfo(selectedTowerType);
    }
}
