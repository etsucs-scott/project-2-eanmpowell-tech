using System;
using System.Collections.Generic;
using System.Linq;

namespace WarGame.Core
{
    public class WarGameEngine
    {
        // Stores all players and their hands
        private PlayerHands players = new PlayerHands();

        //Shared pot that collects all played cards during rounds and ties
        private List<Card> pot = new List<Card>();

        //Tracks how many rounds have been played
        private int roundCount = 0;

        //Maximum number of rounds before forced game end
        private int maxRounds;

        //Determines whether the game pauses between rounds (step mode)
        private bool stepMode;

        public WarGameEngine(List<string> playerNames, int maxRounds = 10000, bool stepMode = false)
        {
            this.maxRounds = maxRounds;
            this.stepMode = stepMode;

            //Initialize a hand for each player
            foreach (var name in playerNames)
            {
                players.Hands[name] = new Hand();
            }

            InitializeGame();
        }

        //Creates a new deck and distributes cards evenly among players
        //Cards are dealt one at a time in round-robin order
        private void InitializeGame()
        {
            var deck = new Deck();
            var playerList = players.Hands.Keys.ToList();
            int index = 0;

            while (deck.Count > 0)
            {
                players.Hands[playerList[index]].AddCard(deck.Draw());
                index = (index + 1) % playerList.Count;
            }
        }

        //Main game loop: continues until one player remains or round limit is reached
        public void PlayGame()
        {
            while (players.Hands.Count > 1 && roundCount < maxRounds)
            {
                PlayRound();
                roundCount++;
            }

            Console.WriteLine("\n--- Game Over ---");
        }

        //Executes a single round where each player plays one card
        //Handles elimination of players with no cards at the start of the round
        private void PlayRound()
        {
            Console.WriteLine($"\nRound {roundCount + 1}");

            //Remove players who have no cards left
            foreach (var p in players.Hands.Keys.ToList())
            {
                if (players.Hands[p].Count == 0)
                {
                    players.Hands.Remove(p);
                }
            }

            var played = new Dictionary<string, Card>();

            //Each player plays their top card
            foreach (var player in players.Hands)
            {
                var card = player.Value.PlayCard();
                played[player.Key] = card;
                pot.Add(card); // Add card to the shared pot

                Console.WriteLine($"{player.Key}: {card.ShortRank()}");
            }

            //Determine the outcome of the round
            ResolveRoundFormatted(played);

            //Pause only if step mode is enabled
            if (stepMode)
            {
                Console.WriteLine("\nPress SPACE for next round...");

                // Wait until the user presses SPACE
                while (Console.ReadKey(true).Key != ConsoleKey.Spacebar)
                {
                    // do nothing
                }
            }
        }

        //Determines the winner of a round or initiates a tie (war)
        //If one winner exists, they collect the entire pot
        //If multiple players tie, a tiebreaker is triggered
        private void ResolveRoundFormatted(Dictionary<string, Card> played)
        {
            int highest = played.Max(p => (int)p.Value.Rank);

            //Find all players who have the highest card
            var winners = played
                .Where(p => (int)p.Value.Rank == highest)
                .Select(p => p.Key)
                .ToList();

            if (winners.Count == 1)
            {
                string winner = winners[0];

                Console.WriteLine($"Winner: {winner} ({FormatCardCounts()})");

                //Winner collects all cards in the pot
                players.Hands[winner].AddCards(pot);
                pot.Clear();
            }
            else
            {
                Console.WriteLine($"Tie between {string.Join(" and ", winners)}!");

                //Print pot BEFORE adding tiebreaker cards
                Console.WriteLine("Pot includes: " + string.Join(", ", pot.Select(c => c.ShortRank())));

                HandleTieFormatted(winners);
            }
        }

        //Handles tie situations by having only tied players play additional cards
         //Cards from tiebreakers are added to the pot
        //If a player cannot continue, they are eliminated
         //Recursively resolves until a winner is found
        private void HandleTieFormatted(List<string> tiedPlayers)
        {
            var nextRound = new Dictionary<string, Card>();

            Console.Write("Tiebreaker: ");

            bool first = true;

            foreach (var player in tiedPlayers)
            {
                //If player has no cards, they are eliminated
                if (!players.Hands.ContainsKey(player) || players.Hands[player].Count == 0)
                {
                    players.Hands.Remove(player);
                    continue;
                }

                var card = players.Hands[player].PlayCard();

                //Add tiebreaker card to the pot
                pot.Add(card);

                nextRound[player] = card;

                if (!first)
                    Console.Write(" | ");

                Console.Write($"{player}: {card.ShortRank()}");
                first = false;
            }

            Console.WriteLine();

            //Recursively resolve the tiebreaker round
            if (nextRound.Count > 0)
            {
                ResolveRoundFormatted(nextRound);
            }
        }

        //Formats player card counts for display (e.g., P1=26, P2=12)
        private string FormatCardCounts()
        {
            return "Cards: " + string.Join(", ",
                players.Hands.Select(p => $"{p.Key.Replace("Player ", "P")}={p.Value.Count}")
            );
        }

        //Determines the winner at the end of the game
       //If one player remains → they win
        //If round limit reached → player with most cards wins
          //If tied → draw
        public string GetWinner()
        {
            if (players.Hands.Count == 0)
                return "No players left";

            if (players.Hands.Count == 1)
                return players.Hands.Keys.First();

            int maxCards = players.Hands.Max(p => p.Value.Count);

            var topPlayers = players.Hands
                .Where(p => p.Value.Count == maxCards)
                .Select(p => p.Key)
                .ToList();

            if (topPlayers.Count == 1)
                return topPlayers[0];

            return "Draw";
        }
    }
}