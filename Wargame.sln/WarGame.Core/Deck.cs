using System;
using System.Collections.Generic;
using System.Linq;
//Represents a standard deck of cards, all 52 of them
namespace WarGame.Core
{
    public class Deck
    {
        private Stack<Card> cards;
        private static Random rng = new Random();

        public Deck()
        {
            var allCards = new List<Card>();
            //generates every card we can have in the standard deck
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    allCards.Add(new Card(suit, rank));
                }
            }

            var shuffled = allCards.OrderBy(c => rng.Next()).ToList();
            cards = new Stack<Card>(shuffled);
        }

        public int Count => cards.Count;
        //removes cards and returns them to the deck
        public Card Draw()
        {
            return cards.Pop();
        }
    }
}