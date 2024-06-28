namespace BlackJackConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameLogic game = new GameLogic();
            game.Tasks();
            game.Start();
        }
    }
}
