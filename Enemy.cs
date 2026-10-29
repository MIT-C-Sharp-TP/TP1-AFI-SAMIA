public abstract class Enemy
{
    public string Name { get; }
    public int Health { get; private set; }
    public bool IsAlive => Health > 0;

    protected Enemy(string name, int health)
    {
        Name = name;
        Health = health;
    }

    
public virtual void TakeDamage(int amount)
{
    Health = Math.Max(0, Health - amount);
    Console.WriteLine($"{Name} takes {amount} damage ({Health} HP)");
}

    // no body: each kind of enemy attacks in its own way
    public abstract void Attack(Player target, Random rng);
}
