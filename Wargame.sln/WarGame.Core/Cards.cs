using System;
//This shows the possible suits and ranks of our cards!
namespace WarGame.Core
{
    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public enum Rank
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public class Card : IComparable<Card>
    {
        public Suit Suit { get; }
        public Rank Rank { get; }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        // Compare cards by rank (Ace is highest)
        public int CompareTo(Card other)
        {
            return Rank.CompareTo(other.Rank);
        }

        // Short format for assignment output (K, Q, J, etc.)
        public string ShortRank()
        {
            return Rank switch
            {
                Rank.Two => "2",
                Rank.Three => "3",
                Rank.Four => "4",
                Rank.Five => "5",
                Rank.Six => "6",
                Rank.Seven => "7",
                Rank.Eight => "8",
                Rank.Nine => "9",
                Rank.Ten => "10",
                Rank.Jack => "J",
                Rank.Queen => "Q",
                Rank.King => "K",
                Rank.Ace => "A",
                _ => "?"
            };
        }

        // Optional: full description (not used in final output)
        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }
}