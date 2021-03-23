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

    private float timeSinceLastFire;

    public void Start()
    {
        NewRound();
    }

    /// <summary>
    /// Keeps track of how long since the last time this tower shot
    /// </summary>
    public void Update()
    {
        if (active && timeSinceLastFire < currentFireRate)
            timeSinceLastFire += Time.deltaTime;
    }

    /// <summary>
    /// Returns whether this tower can fire this frame
    /// </summary>
    /// <returns></returns>
    public bool CanFire()
    {
        return active && timeSinceLastFire >= currentFireRate;
    }

    /// <summary>
    /// Updates values that change when the tower fires
    /// </summary>
    public void Fire()
    {
        timeSinceLastFire = 0;
        if(health > 0)
        {
            health--;
            if(health == 0)
            {
                active = false;
            }
        }
    }

    /// <summary>
    /// For towers whose fire rates or damage varies between rounds, choose a new one for the new round.
    /// </summary>
    public void NewRound()
    {
        active = true;

        int minFireRate = Registry.towerDefinitions[type].minFireRate;
        int maxFireRate = Registry.towerDefinitions[type].maxFireRate;

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
        else
        {
            fireRate = maxFireRate;
        }

        currentDamage = Random.Range(Registry.towerDefinitions[type].minDamage, Registry.towerDefinitions[type].maxDamage);

        health = Registry.towerDefinitions[type].ContainsEffect(TowerEffectType.PlaceOnPath);

        timeSinceLastFire = currentFireRate;
    }
}
