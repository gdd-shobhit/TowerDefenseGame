using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Targetting types for towers
/// </summary>
public enum TargettingType
{
    Single,
    AllInRange
}

/// <summary>
/// The information about a single tower type
/// </summary>
public class TowerDefinition
{
    public string name;
    public int cost;
    public int range;
    public TargettingType targettingType;
    //Negative fire rates are fractional (-2 = 1/2, -3 = 1/3, etc)
    public int minFireRate;
    public int maxFireRate;
    public int minDamage;
    public int maxDamage;
    public List<TowerEffectDef> towerEffects;
    private GameObject prefab;

    public TowerDefinition(string name, int cost, int range, TargettingType targettingType, int minFireRate, int maxFireRate, int minDamage, int maxDamage, List<TowerEffectDef> towerEffects = null)
    {
        this.name = name;
        this.cost = cost;
        this.range = range;
        this.targettingType = targettingType;
        this.minFireRate = minFireRate;
        this.maxFireRate = maxFireRate;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.towerEffects = towerEffects;
        if (this.towerEffects == null)
            this.towerEffects = new List<TowerEffectDef>();
        prefab = Resources.Load<GameObject>("Prefabs/Towers/" + name);
    }

    /// <summary>
    /// Returns the modifier value if the tower has a given effect
    /// </summary>
    /// <param name="type">Type of effect to check for</param>
    /// <returns>The modifier of the effect, 0 if it doesn't have the given type</returns>
    public int ContainsEffect(TowerEffectType type)
    {
        foreach(TowerEffectDef effectDef in towerEffects)
        {
            if (effectDef.effectType == type)
                return effectDef.modifier;
        }
        return 0;
    }

    /// <summary>
    /// Generates a tower instance from a tower template
    /// </summary>
    /// <returns>A new tower instance of the given type</returns>
    public GameObject GenerateInstance(bool active)
    {
        GameObject tower = GameObject.Instantiate(prefab);
        prefab.SetActive(active);
        return tower;
    }
}
