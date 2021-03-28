using System;
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
    public EnemyManager enemyManager;
    public GameObject StartRoundsButton;
    public bool inRound = false;
    public TowerInstance playerControlledTower;

    public GameObject RangeQuad;
    public GameObject RangeQuadCopy;

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
            RangeQuad.transform.localScale = new Vector3(1, 1, 1);
            towerInstances.Add(playerControlledTower);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Fires any towers that can fire this frame
    /// </summary>
    void Update()
    {
        if (inRound)
        {
            foreach (TowerInstance tower in towerInstances)
            {
                if (tower.CanFire())
                {
                    List<EnemyInstance> enemiesInRange = enemyManager.GetEnemiesAroundTower(tower);
                    if (enemiesInRange.Count == 0)
                        continue;
                    if (Registry.towerDefinitions[tower.type].targettingType == TargettingType.Single)
                    {
                        float AoERange = Registry.towerDefinitions[tower.type].ContainsEffect(TowerEffectType.ExplosiveShot);
                        if (AoERange == 0)
                            enemiesInRange.RemoveRange(1, enemiesInRange.Count - 1);
                        else
                        {
                            enemiesInRange = enemyManager.GetEnemiesAroundPosition(enemiesInRange[0].transform.position, 2.5f + 5 * (AoERange - 1));
                        }
                    }
                    foreach (EnemyInstance enemy in enemiesInRange)
                    {
                        if (enemy.TakeDamage(tower.currentDamage))
                            Debug.Log("YESSSSSS");
                            enemyManager.DestroyEnemyInstance(enemy);
                    }
                    tower.Fire();
                }
            }
        }
    }

    /// <summary>
    /// Starts spawning enemies
    /// </summary>
    public void StartRounds()
    {
        inRound = true;
        StartRoundsButton.SetActive(false);
    }

    /// <summary>
    /// Registers when a tile is clicked on and executes actions based on that
    /// </summary>
    /// <param name="position">The tile that was clicked</param>
    public void TileClicked(Vector2Int position)
    {
        //Reset Selector Quad
        RangeQuad.transform.localScale = new Vector3(1, 1, 1);
        Destroy(RangeQuadCopy);

        selectedTile = tileManager.tiles[position.x, position.y];
        //If that tile already has a tower on it
        if (selectedTile.IsTaken())
        {
            selectedTowerType = TowerType.None;
            DisplayTowerInfo(selectedTile.occupyingTower);
            DisplayTowerRange(selectedTile.occupyingTower, selectedTile.transform.position);
        }
        //If placing nothing or unable to place on the selected tile
        else if (selectedTowerType == TowerType.None || (selectedTile.GetPathTile() != Registry.towerDefinitions[selectedTowerType].PlacableOnPath()) || currencyCount < Registry.towerDefinitions[selectedTowerType].cost)
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
            DisplayTowerRange(selectedTile.occupyingTower, selectedTile.transform.position);
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
            textList[5].text = "";
            textList[6].text = "";
            return;
        }
        TowerDefinition towerInfo = Registry.towerDefinitions[type];
        textList[0].text = towerInfo.name;
        textList[1].text = "Cost: " + towerInfo.cost;
        textList[2].text = "Range: " + towerInfo.range;
        textList[3].text = "Fire Rate: " + (towerInfo.minFireRate == towerInfo.maxFireRate ? "" + towerInfo.maxFireRate : (towerInfo.minFireRate + " - " + towerInfo.maxFireRate));
        textList[4].text = "Damage: " + (towerInfo.minDamage == towerInfo.maxDamage ? "" + towerInfo.maxDamage : (towerInfo.minDamage + " - " + towerInfo.maxDamage));
        textList[5].text = "Targetting Type: " + (towerInfo.targettingType == TargettingType.Single ? "Single Target" : "All in Range");
        textList[6].text = "";
        foreach (TowerEffectType effect in Enum.GetValues(typeof(TowerEffectType)))
        {
            if (towerInfo.ContainsEffect(effect) != 0)
                textList[6].text += Registry.towerEffectStrings[effect];
        }

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
        textList[5].text = "Targetting Type: " + (towerInfo.targettingType == TargettingType.Single ? "Single Target" : "All in Range");
        textList[6].text = "";
        foreach (TowerEffectType effect in Enum.GetValues(typeof(TowerEffectType)))
        {
            if (towerInfo.ContainsEffect(effect) != 0)
                textList[6].text += Registry.towerEffectStrings[effect];
        }
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

    /// <summary>
    /// Displays a tower's range with visual representation
    /// </summary>
    /// <param name="tower"></param>
    /// <param name="position"></param>
    public void DisplayTowerRange(TowerInstance tower, Vector3 position)
    {
        TowerDefinition towerInfo = Registry.towerDefinitions[tower.type];
        //Equation to display range on tile grid, An = A1 + ( n - 1 ) * 10
        RangeQuad.transform.localScale *= (15 + (towerInfo.range - 1) * 10);
        

        RangeQuadCopy = Instantiate(RangeQuad, position + new Vector3(0, 0, -3), Quaternion.identity);
    }
}
