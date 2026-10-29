public class Player
{
    private int health;

    public string Name { get; }
    public int MaxHealth { get; }
    public int Potions { get; private set; }
    
    public int Health
    {
        get { return health; }
        private set { health = Math.Clamp(value, 0, MaxHealth); }
    }
public bool IsAlive => health > 0;
public Player(string name, int maxHealth, int potions)
{
    Name = name;
    MaxHealth = maxHealth;
    Potions = potions;
    Health = MaxHealth;
}
public event Action<string>? Died;
public void TakeDamage(int amount)
{
    bool wasAlive = IsAlive;

    Health = Math.Max(0, Health - amount);

    Console.WriteLine($"{Name} takes {amount} damage ({Health} HP)");

    if (wasAlive && !IsAlive)
    {
        Died?.Invoke(Name);
    }
}
public void DrinkPotion()
{
    if (Potions <= 0)
    {
        Console.WriteLine("No potion left!");
        return;
    }

    Potions--;
    Health += 30;
    Console.WriteLine($"{Name} drinks a potion ({Health} HP)");
}
}
