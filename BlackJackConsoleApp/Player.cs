using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackConsoleApp
{
    internal class Player
    {
        public List<Card> playerCards { get; set; }
        public string? playerName { get; }
        public Player(List<Card> _playerCards, string? _playerName)
        {
            playerCards = _playerCards;
            playerName = _playerName;
        }
    }
}
