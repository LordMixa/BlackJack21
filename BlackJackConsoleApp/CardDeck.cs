using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackConsoleApp
{
    internal class CardDeck
    {
        private List<Card> cards;

        public CardDeck()
        {
            GenerateDeck();
        }

        public void GenerateDeck()
        {
            cards = new List<Card>();
            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    cards.Add(new Card(value,suit));
                }
            }
        }

        public void MixCards()
        {
            Random rand = new Random();
            cards = cards.OrderBy(card => rand.Next()).ToList();
        }

        public List<int> FindAcesPositions()
        {
            List<int> positions = new List<int>();
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].Value == CardValue.Ace)
                {
                    positions.Add(i);
                }
            }
            return positions;
        }

        public void SortSpadesStart()
        {
            var spades = cards.Where(card => card.Suit == CardSuit.Spades).ToList();
            var nonSpades = cards.Where(card => card.Suit != CardSuit.Spades).ToList();
            cards = spades.Concat(nonSpades).ToList();
        }

        public void SortDeck()
        {
            cards = cards.OrderBy(card => card.Suit).ThenBy(card => card.Value).ToList();
        }

        public void PrintDeck()
        {
            foreach (var card in cards)
            {
                Console.WriteLine(card);
            }
        }

        public Card DrawCard()
        {
            if (cards.Count == 0) throw new InvalidOperationException("Deck is empty.");
            var card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
    }
}
