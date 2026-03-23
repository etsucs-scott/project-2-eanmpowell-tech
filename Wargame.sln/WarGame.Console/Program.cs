using WarGame.Core;
using System;
using System.Collections.Generic;

Console.Write("Enter number of players (2-4): ");
int numPlayers;

while (!int.TryParse(Console.ReadLine(), out numPlayers) || numPlayers < 2 || numPlayers > 4)
{
    Console.Write("Invalid input. Enter 2, 3, or 4: ");
}

// 🎮 Mode selection
Console.WriteLine("\nSelect mode:");
Console.WriteLine("Press ENTER for fast mode");
Console.WriteLine("Press SPACE for step-by-step mode");

bool stepMode = false;

var key = Console.ReadKey(true);
if (key.Key == ConsoleKey.Spacebar)
{
    stepMode = true;
}

Console.WriteLine(stepMode ? "\nStep mode selected\n" : "\nFast mode selected\n");

// Default player names
var players = new List<string>();
for (int i = 1; i <= numPlayers; i++)
{
    players.Add($"Player {i}");
}

// 👇 pass stepMode into engine
var game = new WarGameEngine(players, 10000, stepMode);

game.PlayGame();

Console.WriteLine($"\nWinner: {game.GetWinner()}");