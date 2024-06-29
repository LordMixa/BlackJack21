namespace BlackJackConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameLogic game = new GameLogic();
            game.Start();
            Console.WriteLine("Check Tasks?(yes/no)");
            string check = Console.ReadLine();
            if (check != null && check != "")
            {
                if (check.ToLower() == "yes" || check.ToLower() == "y")
                {
                    Tasks tasks = new Tasks();
                }
            }
        }
    }
}
