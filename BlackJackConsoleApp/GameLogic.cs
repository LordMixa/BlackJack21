namespace BlackJackConsoleApp
{

    public class GameLogic
    {
        private CardDeck deck;
        private List<Card> playerCards;
        private List<Card> computerCards;
        private bool playerTurn;
        private int playerWins;
        private int computerWins;
        private int draws;
        private string? playerName;
        private bool showComputerhand;

        public GameLogic()
        {
            showComputerhand = false;
            deck = new CardDeck();
            playerCards = new List<Card>();
            computerCards = new List<Card>();
            playerWins = 0;
            computerWins = 0;
            draws = 0;
        }
        public void Tasks()
        {
            Console.WriteLine("Created sorted deck");
            deck.PrintDeck();
            Console.WriteLine("______________________________________________________________");
            Console.WriteLine("Mixed deck");
            deck.MixCards();
            deck.PrintDeck();
            Console.WriteLine("______________________________________________________________");
            List<int> indexs = deck.FindAcesPositions();
            foreach (int index in indexs)
                Console.WriteLine($"Ace in {index} position");
            Console.WriteLine("______________________________________________________________");
            deck.SortSpadesStart();
            Console.WriteLine("Spades on start deck");
            deck.PrintDeck();
            Console.WriteLine("______________________________________________________________");
            Console.WriteLine("Sorted deck");
            deck.SortDeck();
            deck.PrintDeck();
            Console.WriteLine("______________________________________________________________\n\n");
        }
        public void Start()
        {
            Console.WriteLine("Enter your name");
            playerName = Console.ReadLine();
            if (playerName == null || playerName == "")
                playerName = "Player";
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
                if (response?.ToLower() != "yes" &&  response?.ToLower() != "y")
                    break;
            }
            PrintStatistics();
        }

        private void PlayRound()
        {
            deck.MixCards();
            playerCards.Clear();
            computerCards.Clear();

            Console.WriteLine("Who should draw first? (player/computer)");
            var response = Console.ReadLine();
            if (response.ToLower() == "player" || response == "p")
                playerTurn = true;
            else
                playerTurn = false;

            playerCards.Add(deck.DrawCard());
            playerCards.Add(deck.DrawCard());
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
                else if(!contP&&contC)
                    contC = MakeMoveComp();
                else if (contP && !contC)
                {
                    contP = MakeMovePlayer();
                    PrintHands();
                }
                else
                {
                    if (showComputerhand)
                        PrintHands();
                    else
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
            int playerPoints = CalculatePoints(playerCards);
            if (playerPoints == 20 || playerPoints == 21 || (playerCards.Count == 2 && playerCards.All(card => card.Value == CardValue.Ace)) || playerPoints > 21 )
                return false;
            Console.WriteLine($"{playerName}'s turn. Do you want another card? (yes/no)");
            var response = Console.ReadLine();
            if (response?.ToLower() == "yes" || response?.ToLower() == "y")
            {
                playerCards.Add(deck.DrawCard());
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
            foreach (var card in playerCards)
            {
                Console.WriteLine(card);
            }
            Console.WriteLine($"Total points: {CalculatePoints(playerCards)}\n");
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
            int playerPoints = CalculatePoints(playerCards);
            int computerPoints = CalculatePoints(computerCards);

            Console.WriteLine($"Player's points: {playerPoints}");
            Console.WriteLine($"Computer's points: {computerPoints}");

            if (playerPoints == 21 || (playerCards.Count == 2 && playerCards.All(card => card.Value == CardValue.Ace)))
            {
                Console.WriteLine("Player wins!");
                playerWins++;
            }
            else if (computerPoints == 21 || (computerCards.Count == 2 && computerCards.All(card => card.Value == CardValue.Ace)))
            {
                Console.WriteLine("Computer wins!");
                computerWins++;
            }
            else if (playerPoints > 21 && computerPoints > 21)
            {
                if (playerPoints < computerPoints)
                {
                    Console.WriteLine("Player wins!");
                    playerWins++;
                }
                else if (playerPoints > computerPoints)
                {
                    Console.WriteLine("Computer wins!");
                    computerWins++;
                }
                else
                {
                    Console.WriteLine("It's a draw!");
                    draws++;
                }
            }
            else if (playerPoints > 21)
            {
                Console.WriteLine("Computer wins!");
                computerWins++;
            }
            else if (computerPoints > 21)
            {
                Console.WriteLine("Player wins!");
                playerWins++;
            }
            else if (playerPoints < computerPoints)
            {
                Console.WriteLine("Computer wins!");
                computerWins++;
            }
            else if (playerPoints > computerPoints)
            {
                Console.WriteLine("Player wins!");
                playerWins++;
            }
            else
            {
                Console.WriteLine("It's a draw!");
                draws++;
            }
        }

        private void PrintStatistics()
        {
            Console.WriteLine("Game statistics:");
            Console.WriteLine($"Player wins: {playerWins}");
            Console.WriteLine($"Computer wins: {computerWins}");
            Console.WriteLine($"Draws: {draws}");
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
