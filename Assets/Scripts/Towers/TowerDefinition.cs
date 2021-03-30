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
    public string name3D;
    public int cost;
    public int range;
    public TargettingType targettingType;
    //Negative fire rates are fractional (-2 = 1/2, -3 = 1/3, etc)
    public int minFireRate;
    public int maxFireRate;
    public int minDamage;
    public int maxDamage;
    public ParticleSystem particleEffect;
    public List<TowerEffectDef> towerEffects;
    private GameObject prefab;
    private GameObject prefab3D;

    public TowerDefinition(string name,string name3D, int cost, int range, TargettingType targettingType, int minFireRate, int maxFireRate, int minDamage, int maxDamage, List<TowerEffectDef> towerEffects = null)
    {
        this.name = name;
        this.name3D = name3D;
        this.cost = cost;
        this.range = range;
        this.targettingType = targettingType;
        this.minFireRate = minFireRate;
        this.maxFireRate = maxFireRate;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.towerEffects = towerEffects;
        this.particleEffect = particleEffect;
        if (this.towerEffects == null)
            this.towerEffects = new List<TowerEffectDef>();
        prefab = Resources.Load<GameObject>("Towers/" + name);
        prefab3D = Resources.Load<GameObject>("Towers/" + name3D);
    }

    /// <summary>
    /// Returns the modifier value if the tower has a given effect
    /// </summary>
    /// <param name="type">Type of effect to check for</param>
    /// <returns>The modifier of the effect, 0 if it doesn't have the given type</returns>
    public int ContainsEffect(TowerEffectType type)
    {
        foreach (TowerEffectDef effectDef in towerEffects)
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
    public List<GameObject> GenerateInstance(bool active)
    {
        List<GameObject> towers = new List<GameObject>();
        towers.Add(GameObject.Instantiate(prefab));
        towers.Add(GameObject.Instantiate(prefab3D));
        prefab.SetActive(active);
        prefab3D.SetActive(active);
        return towers;
    }

    /// <summary>
    /// Returns whether this tower can be placed on the path
    /// </summary>
    public bool PlacableOnPath()
    {
        return ContainsEffect(TowerEffectType.PlaceOnPath) > 0;
    }
}
