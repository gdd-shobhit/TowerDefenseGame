/// <summary>
/// The information about a single enemy type
/// </summary>
public class EnemyDefinition
{
    public int health;
    public float moveSpeed;

    public EnemyDefinition(int health, float moveSpeed)
    {
        this.health = health;
        this.moveSpeed = moveSpeed;
    }
}
