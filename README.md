[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/hZIAsDPT)
# CSCI 1260 — Project

## Project Instructions
All project requirements, grading criteria, and submission details are provided on **D2L**.  
Refer to D2L as the *authoritative source* for this assignment.

This repository is intentionally minimal. You are responsible for:
- Creating the solution and projects
- Designing the class structure
- Implementing the required functionality

---

## Getting Started (CLI)

You may use **Visual Studio**, **VS Code**, or the **terminal**.

### Create a solution
```bash
dotnet new sln -n ProjectName
```

### Create a project (example: console app)
```bash
dotnet new console -n ProjectName.App
```

### Add the project to the solution
```bash
dotnet sln add ProjectName.App
```

### Build and run
```bash
dotnet build
dotnet run --project ProjectName.App
```

## Notes
- Commit early and commit often.
- Your repository history is part of your submission.
- Update this README with build/run instructions specific to your project.

//Instructions

Starting the Game

To start the game, first pick your amount of players by using the number buttons. The program only has a range of 2-4 players.

//Mode Selection 

Next, you will have the choice of fast mode and step-by-step mode.
The Fast mode will speed through all rounds until we hit the limit of 10,000 rounds.
The step-by-step mode will go round by round, going at the speed the player would like, moving round by round with the push of the
space button.
To select between these modes, use the ENTER key for fast mode, and the SPACE key for step-by-step mode.

//Tiebreaker rules

If there is a tie, then the tiebreaker pot is drawn, where the cards from the players that didn't draw are added, plus one 
additional card from the tied players, which are then added to the pot. Whichever player has the highest tiebreaker card is 
declared the winner, and recieves the entire pot. If there is another tie, repeat the process with
the tied players until we have a winner or they run out of cards.

//Win Conditions

The win conditions are as follows: We either reach the round limit, which is 10,000 rounds, or we have one player left with cards.
If a player runs out of cards, they are eliminated.
When a player is eliminated, they cannot participate in any further rounds.
In the event of reaching the round limit, the winner is decided by seeing which player has the most cards, who is then declared the winner.
