public class Boss : Enemy
{
    public int Armour { get; } = 6;

    public Boss() : base("Boss", 60)
    {
    }

    public override void TakeDamage(int amount)
    {
        int reduced = Math.Max(amount - Armour, 1);

        Console.WriteLine($"The armour absorbs {amount - reduced} damage.");

        base.TakeDamage(reduced);
    }

    public override void Attack(Player target, Random rng)
    {
        Console.WriteLine("Boss slams the ground!");
        target.TakeDamage(rng.Next(9, 17));
    }
}
