using System.Linq;

namespace MathGame
{
    internal class Helpers
    {

        private static readonly Random random = new Random();
        private static Helpers helpers = new Helpers();

        internal static void HoldForEnter()
        {
            Console.Write(" Press enter to continue.");
            Console.ReadLine();
        }

        internal static int UserIntInput()
        {
            string? userInput;
            int result;

            userInput = Console.ReadLine();

            while ( string.IsNullOrWhiteSpace(userInput) || !int.TryParse(userInput, out result)) 
            {
                helpers.TypeString("No go! must be an interger");
                userInput = Console.ReadLine();
            }
            return result;
        }

        internal static bool CheckAnswer(int result, int answer) 
        {
            if (result == answer) 
            {
                helpers.TypeString("Nice job! that was correct!");
                return true;
            }
            helpers.TypeString("Sorry that was not the answer I was hoping for, Try Again!");
            return false;
        }

        internal static int GetRandomInt(GameDifficulty difficulty)
        {
            switch (difficulty)
            {
                case GameDifficulty.Easy:
                    return random.Next(1, 9);
                case GameDifficulty.Medium:
                    return random.Next(5, 100);
                case GameDifficulty.Hard:
                    return random.Next(50, 200);
                    default:
                    return random.Next(1, 5);
            }
        }


        internal void TypeString(string stringToType, int delay = 30)
        {
            foreach (char c in stringToType)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }
    }
}
