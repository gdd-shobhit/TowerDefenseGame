/// <summary>
/// The types of special effects a tower can have
/// </summary>
public enum TowerEffectType
{
    //Modifier is the range of the explosion (0 = only tile target is on)
    ExplosiveShot,
    //Modifier is the tower health
    PlaceOnPath
}

/// <summary>
/// Information on a special effect of a tower
/// </summary>
public class TowerEffectDef
{
    public TowerEffectType effectType;
    public int modifier;

    public TowerEffectDef(TowerEffectType effectType, int modifier)
    {
        this.effectType = effectType;
        this.modifier = modifier;
    }
}