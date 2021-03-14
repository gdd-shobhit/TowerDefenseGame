using System.Collections.Generic;

/// <summary>
/// Tower type names for readability
/// </summary>
public enum TowerType
{
    Basic,
    Bomb
}

/// <summary>
/// Contains the templates for each tower type and can make instances from them
/// </summary>
public class TowerRegistry
{
    public static Dictionary<TowerType, TowerDefinition> towerDefinitions = new Dictionary<TowerType, TowerDefinition>()
    {
        {
            TowerType.Basic,
            new TowerDefinition
            (
                name: "Basic Tower",
                cost: 1,
                range: 2,
                targettingType: TargettingType.Single,
                minFireRate: 1,
                maxFireRate: 1,
                minDamage: 2,
                maxDamage: 2
            )
        },
        {
            TowerType.Bomb,
            new TowerDefinition
            (
                name: "Bomb Tower",
                cost: 3,
                range: 2,
                targettingType: TargettingType.Single,
                minFireRate: -2,
                maxFireRate: -2,
                minDamage: 2,
                maxDamage: 2,
                towerEffects: new List<TowerEffectDef>()
                {
                    new TowerEffectDef(TowerEffectType.ExplosiveShot, 1)
                }
            )
        }
    };

    /// <summary>
    /// Generates a tower instance from a tower template
    /// </summary>
    /// <param name="towerType">The tower template to use</param>
    /// <returns>A tower instance of the given type</returns>
    public static TowerInstance GenerateInstance(TowerType towerType, bool active = true)
    {
        return new TowerInstance(towerType, active);
    }
}
