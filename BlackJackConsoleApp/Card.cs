using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackConsoleApp
{
    public struct Card
    {
        public CardValue Value { get; set; }
        public CardSuit Suit { get; set; }
        public Card(CardValue value, CardSuit suit) 
        {
            Value = value;
            Suit = suit;
        }
        public override string ToString() 
        { 
            return Suit.ToString()+" "+Value.ToString();
        }
    }
    public enum CardValue
    {
        Jack = 2,
        Queen,
        King,
        Six = 6,
        Seven,
        Eight,
        Nine,
        Ten,
        Ace
    }
    public enum CardSuit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }
}
