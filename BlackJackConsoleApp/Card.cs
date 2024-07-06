using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackConsoleApp
{
    public struct Card
    {
        public CardValue Value { get; }
        public CardSuit Suit { get; }
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
}
