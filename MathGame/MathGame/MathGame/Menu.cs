
namespace MathGame
{
    internal class Menu
    {
        private static Helpers helpers = new Helpers();
        GameEngine engine = new GameEngine();
        string options = @"Please choose your next step:
            0 - Change Difficulty
            1 - Addition Game
            2 - Subtraction Game
            3 - Division Game
            4 - Multiplication Game
            5 - Resgin";

        string DifficultyMenu = @"Change the game difficulty:
            0 - Return to Menu
            1 - Easy
            2 - Medium
            3 - Hard";

        private GameDifficulty difficulty = GameDifficulty.Easy;
        internal void DisplayMenu(string name)
        {

            while (true) 
            {
                helpers.TypeString($"Welcome {name}?", 100);

                helpers.TypeString(options, 25);


                switch (Helpers.UserIntInput())
                {
                    case 0:
                        difficulty = SelectDifficulty(); 
                        break;
                    case 1:
                        StartGame("Addition Game", engine.AdditionGame, difficulty);
                        break;
                    case 2:
                        StartGame("Subtractio Game", engine.SubtractionGame, difficulty);
                        break;
                    case 3:
                        StartGame("Division Game", engine.DivisionGame, difficulty);
                        break;
                    case 4:
                        StartGame("Multiplication Game", engine.MultiplicationGame, difficulty);
                        break;
                    case 5:
                        helpers.TypeString("GoodBye...", 200);
                        Environment.Exit(0);
                        break;
                }
            }

        }

        private void StartGame(string gameName, Action<GameDifficulty> GameMethod, GameDifficulty difficulty)
        {
            helpers.TypeString($"Your {gameName} will now begin...", 100);
            GameMethod(difficulty);
        }
        internal GameDifficulty SelectDifficulty()
        {
            while (true) 
            {
                Console.WriteLine(DifficultyMenu);

                switch (Helpers.UserIntInput())
                {
                    case 0:
                        helpers.TypeString("Returning to the Main Menu...");
                        return difficulty; 
                    case 1:
                        helpers.TypeString("Game Difficulty is set to Easy.");
                        return GameDifficulty.Easy;
                    case 2:
                        helpers.TypeString("Game Difficulty is set to Medium.");
                        return GameDifficulty.Medium;
                    case 3:
                        helpers.TypeString("Game Difficulty is set to Hard.");
                        return GameDifficulty.Hard;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }
    }
}
