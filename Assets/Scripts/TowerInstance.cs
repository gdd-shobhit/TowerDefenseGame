using UnityEngine;

/// <summary>
/// An instance of a tower on the map
/// </summary>
public class TowerInstance : MonoBehaviour
{
    public TowerType type;
    public int currentDamage;
    private int fireRate;
    public bool active;
    public float currentFireRate { get { return fireRate > 0 ? fireRate : -1.0f / fireRate; } }
    public int health;

    public TowerInstance(TowerType type, bool active = true)
    {
        this.type = type;
        this.active = active;
        NewRound();
    }

    /// <summary>
    /// For towers whose fire rates or damage varies between rounds, choose a new one for the new round.
    /// </summary>
    public void NewRound()
    {
        int minFireRate = TowerRegistry.towerDefinitions[type].minFireRate;
        int maxFireRate = TowerRegistry.towerDefinitions[type].maxFireRate;

        if (minFireRate != maxFireRate)
        {
            //Fire rate cannot be 0 or -1 so some special math needs to be done
            bool fireRateBridgesZero = minFireRate < 0 && maxFireRate > 0;
            fireRate = fireRateBridgesZero ? Random.Range(minFireRate + 2, maxFireRate) : Random.Range(minFireRate, maxFireRate);
            if (fireRateBridgesZero && fireRate < 1)
            {
                fireRate -= 2;
            }
        }

        currentDamage = Random.Range(TowerRegistry.towerDefinitions[type].minDamage, TowerRegistry.towerDefinitions[type].maxDamage);

        health = Mathf.Max(TowerRegistry.towerDefinitions[type].ContainsEffect(TowerEffectType.PlaceOnPath), 1);
    }
}
