
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

int PlayGame()
{
    Player player = new Player("Aya", 100, 2);
    player.Died += name => Console.WriteLine($"Game over for {name}.");

    List<Enemy> enemies = new List<Enemy>();
    enemies.Add(new Goblin());
    enemies.Add(new Orc());
    enemies.Add(new Goblin());
    enemies.Add(new Boss());

    Random rng = new Random();
    int turn = 0;

    Console.WriteLine("=== Arena ===");

    while (player.IsAlive && enemies.Count > 0)
    {
        turn++;

        Enemy enemy = enemies[0];

        Console.WriteLine();
        Console.WriteLine(
            $"Turn {turn}: {player.Name} {player.Health} HP, " +
            $"{player.Potions} potions | {enemy.Name} {enemy.Health} HP"
        );

        Console.Write("1 attack, 2 drink a potion > ");
        string? text = Console.ReadLine();

        if (!int.TryParse(text, out int choice))
        {
            Console.WriteLine("Type 1 or 2.");
            continue;
        }

        switch (choice)
        {
            case 1:
                enemy.TakeDamage(rng.Next(12, 25));
                break;

            case 2:
                player.DrinkPotion();
                break;

            default:
                Console.WriteLine("Type 1 or 2.");
                continue;
        }

        if (!enemy.IsAlive)
        {
            Console.WriteLine($"{enemy.Name} is defeated!");
            enemies.RemoveAt(0);
            continue;
        }

        enemy.Attack(player, rng);
    }

    if (player.IsAlive)
    {
        Console.WriteLine($"Victory in {turn} turns!");
        return turn;
    }
    else
    {
        Console.WriteLine("Defeat...");
        return 0;
    }
}

void SaveResult(string line)
{
    try
    {
        File.AppendAllText("results.txt", line + Environment.NewLine);
        Console.WriteLine($"Saved: {line}");
    }
    catch (IOException e)
    {
        Console.WriteLine($"Could not save the result: {e.Message}");
    }
    catch (UnauthorizedAccessException e)
    {
        Console.WriteLine($"Could not save the result: {e.Message}");
    }
}

List<int> wins = new List<int>();
int game = 0;

while (true)
{
    game++;

    int turns = PlayGame();

    if (turns > 0)
    {
        wins.Add(turns);
        SaveResult($"Game {game}: victory in {turns} turns");
    }
    else
    {
        SaveResult($"Game {game}: defeat");
    }

    Console.Write("Play again? (y/n) > ");

    if (Console.ReadLine() != "y")
    {
        break;
    }
}

Console.WriteLine($"Victories: {wins.Count} of {game}");

foreach (int t in wins.OrderBy(t => t).Take(3))
{
    Console.WriteLine($"- {t} turns");
}
