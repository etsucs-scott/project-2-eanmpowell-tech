using System.Collections.Generic;
//Represents the player's hand using a queue (FIFO specifically)
namespace WarGame.Core
{
    public class Hand
    {
        private Queue<Card> cards = new Queue<Card>();
        //# of cards in hand currently
        public int Count => cards.Count;
        //Adds one new card to the back of the hand
        public void AddCard(Card card)
        {
            cards.Enqueue(card);
        }
        //adds multiple cards to the hand (Use when winning the pot)
        public void AddCards(IEnumerable<Card> newCards)
        {
            foreach (var card in newCards)
            {
                cards.Enqueue(card);
            }
        }
        //Plays (and technically removes) the top card from the hand
        public Card PlayCard()
        {
            return cards.Dequeue();
        }
    }
}