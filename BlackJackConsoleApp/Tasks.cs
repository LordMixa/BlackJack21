using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackConsoleApp
{
    public class Tasks
    {
        public Tasks()
        {
            CardDeck deck = new CardDeck();
            Console.WriteLine("Task 1");
            Console.WriteLine("Created sorted deck");
            deck.PrintDeck();
            Console.WriteLine("______________________________________________________________/n");
            Console.WriteLine("Task 2");
            Console.WriteLine("Mixed deck");
            deck.MixCards();
            deck.PrintDeck();
            Console.WriteLine("______________________________________________________________/n");
            Console.WriteLine("Task 3");
            List<int> indexs = deck.FindAcesPositions();
            foreach (int index in indexs)
                Console.WriteLine($"Ace in {index} position");
            Console.WriteLine("______________________________________________________________/n");
            Console.WriteLine("Task 4");
            deck.SortSpadesStart();
            Console.WriteLine("Spades on start deck");
            deck.PrintDeck();
            Console.WriteLine("______________________________________________________________/n");
            Console.WriteLine("Task 5");
            Console.WriteLine("Sorted deck");
            deck.SortDeck();
            deck.PrintDeck();
            Console.WriteLine("______________________________________________________________\n\n");
        }
    }
}
