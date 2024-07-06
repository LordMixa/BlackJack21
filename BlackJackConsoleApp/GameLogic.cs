namespace BlackJackConsoleApp
{

    public class GameLogic
    {
        private CardDeck deck;
        private List<Card> computerCards;
        private bool playerTurn;
        private Statistic statistic;
        private bool showComputerhand;
        private Player player;

        public GameLogic()
        {
            showComputerhand = false;
            deck = new CardDeck();
            computerCards = new List<Card>();
            statistic = new Statistic();
        }
        public void Start()
        {
            Console.WriteLine("Enter your name");
            string playerName = Console.ReadLine();
            if (playerName == null || playerName == "" || playerName.Length < 2)
                playerName = "Player";
            player = new Player(new List<Card>(), "Player");
            Console.WriteLine("Show computer`s hand?(yes/no)");
            string check = Console.ReadLine();
            if (check != null && check != "")
            {
                if (check.ToLower() == "yes" || check.ToLower() == "y")
                    showComputerhand = true;
            }
            while (true)
            {
                PlayRound();
                Console.WriteLine("Do you want to play another round? (yes/no)");
                var response = Console.ReadLine();
                if (response?.ToLower() != "yes" && response?.ToLower() != "y")
                    break;
            }
            statistic.PrintStatistics();
        }

        private void PlayRound()
        {
            deck.MixCards();
            player.playerCards.Clear();
            computerCards.Clear();

            Console.WriteLine("Who should draw first? (player/computer)");
            var response = Console.ReadLine();
            if (response.ToLower() == "player" || response == "p")
                playerTurn = true;
            else
                playerTurn = false;

            player.playerCards.Add(deck.DrawCard());
            player.playerCards.Add(deck.DrawCard());
            computerCards.Add(deck.DrawCard());
            computerCards.Add(deck.DrawCard());

            PrintHands();

            bool contP = true;
            bool contC = true;
            while (true)
            {
                if (playerTurn && contC && contP)
                {
                    contP = MakeMovePlayer();
                    contC = MakeMoveComp();
                    PrintHands();
                }
                else if (!playerTurn && contC && contP)
                {
                    contC = MakeMoveComp();
                    contP = MakeMovePlayer();
                    PrintHands();
                }
                else if (!contP && contC)
                    contC = MakeMoveComp();
                else if (contP && !contC)
                {
                    contP = MakeMovePlayer();
                    PrintHands();
                }
                else
                {
                    if (!showComputerhand)
                    {
                        showComputerhand = true;
                        PrintHands();
                        showComputerhand = false;
                    }
                    break;
                }
            }
            DetermineWinner();
        }

        private bool MakeMovePlayer()
        {
            int playerPoints = CalculatePoints(player.playerCards);
            if (playerPoints == 20 || playerPoints == 21 || (player.playerCards.Count == 2 && player.playerCards.All(card => card.Value == CardValue.Ace)) || playerPoints > 21)
                return false;
            Console.WriteLine($"{player.playerName}'s turn. Do you want another card? (yes/no)");
            var response = Console.ReadLine();
            if (response?.ToLower() == "yes" || response?.ToLower() == "y")
            {
                player.playerCards.Add(deck.DrawCard());
                return true;
            }
            else
                return false;
        }
        private bool MakeMoveComp()
        {
            if (ComputerTurn())
            {
                computerCards.Add(deck.DrawCard());
                Console.WriteLine("Computer took card");
                return true;
            }
            else
            {
                Console.WriteLine("Computer ended move");
                return false;
            }
        }
        private void PrintHands()
        {
            Console.WriteLine("\nPlayer's hand:");
            foreach (var card in player.playerCards)
            {
                Console.WriteLine(card);
            }
            Console.WriteLine($"Total points: {CalculatePoints(player.playerCards)}\n");
            if (showComputerhand == true)
            {
                Console.WriteLine("\nComputer's hand:");
                foreach (var card in computerCards)
                {
                    Console.WriteLine(card);
                }
                Console.WriteLine($"Total points: {CalculatePoints(computerCards)}\n");
            }
        }

        private int CalculatePoints(List<Card> hand)
        {
            int points = 0;
            foreach (var card in hand)
            {
                points += card.Value switch
                {
                    CardValue.Ace => 11,
                    CardValue.King => 4,
                    CardValue.Queen => 3,
                    CardValue.Jack => 2,
                    _ => (int)card.Value
                };
            }
            return points;
        }

        private void DetermineWinner()
        {
            int playerPoints = CalculatePoints(player.playerCards);
            int computerPoints = CalculatePoints(computerCards);

            Console.WriteLine($"Player's points: {playerPoints}");
            Console.WriteLine($"Computer's points: {computerPoints}");

            if (playerPoints == 21 || (player.playerCards.Count == 2 && player.playerCards.All(card => card.Value == CardValue.Ace)))
            {
                Console.WriteLine("Player wins!");
                statistic.playerWins++;
            }
            else if (computerPoints == 21 || (computerCards.Count == 2 && computerCards.All(card => card.Value == CardValue.Ace)))
            {
                Console.WriteLine("Computer wins!");
                statistic.computerWins++;
            }
            else if (playerPoints > 21 && computerPoints > 21)
            {
                if (playerPoints < computerPoints)
                {
                    Console.WriteLine("Player wins!");
                    statistic.playerWins++;
                }
                else if (playerPoints > computerPoints)
                {
                    Console.WriteLine("Computer wins!");
                    statistic.computerWins++;
                }
                else
                {
                    Console.WriteLine("It's a draw!");
                    statistic.draws++;
                }
            }
            else if (playerPoints > 21)
            {
                Console.WriteLine("Computer wins!");
                statistic.computerWins++;
            }
            else if (computerPoints > 21)
            {
                Console.WriteLine("Player wins!");
                statistic.playerWins++;
            }
            else if (playerPoints < computerPoints)
            {
                Console.WriteLine("Computer wins!");
                statistic.computerWins++;
            }
            else if (playerPoints > computerPoints)
            {
                Console.WriteLine("Player wins!");
                statistic.playerWins++;
            }
            else
            {
                Console.WriteLine("It's a draw!");
                statistic.draws++;
            }
        }
        public bool ComputerTurn()
        {
            Random random = new Random();
            int computerPoints = CalculatePoints(computerCards);
            if (computerPoints == 21 || computerPoints == 20 || (computerCards.Count == 2 && computerCards.All(card => card.Value == CardValue.Ace)) || computerPoints > 21)
                return false;
            else if (computerPoints <= 10)
                return true;
            else
            {
                int value = 20 - computerPoints;
                int randvalue = random.Next(10);
                if (randvalue < value)
                    return true;
                else
                    return false;
            }
        }
    }
}
