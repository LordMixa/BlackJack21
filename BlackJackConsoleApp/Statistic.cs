using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackConsoleApp
{
    public class Statistic
    {
        public Statistic() 
        {
            playerWins = 0;
            computerWins = 0;
            draws = 0;
        }
        public int playerWins;
        public int computerWins;
        public int draws;
        public void PrintStatistics()
        {
            Console.WriteLine("Game statistics:");
            Console.WriteLine($"Player wins: {playerWins}");
            Console.WriteLine($"Computer wins: {computerWins}");
            Console.WriteLine($"Draws: {draws}");
        }
    }
}
