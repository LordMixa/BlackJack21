using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackConsole
{

    internal class GameLogic
    {
        public static Card[] CreateCards()
        {
            Card[] cards = new Card[36];
            int count = 0;
            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    cards[count++] = new Card(value , suit);
                }
            }
            return cards;
        }
    }
}
