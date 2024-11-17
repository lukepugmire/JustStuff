namespace MathGame
{
    internal class GameEngine
    {
        private static Helpers helpers = new Helpers();

        internal void AdditionGame(GameDifficulty difficulty)
        {
            Console.Clear();
            RunProblem('+', difficulty);

        }

        internal void SubtractionGame(GameDifficulty difficulty) 
        {
            Console.Clear();
            RunProblem('-', difficulty);

        }

        internal void DivisionGame( GameDifficulty difficulty)
        {
            Console.Clear();
            RunProblem('/', difficulty);
        }


        internal void MultiplicationGame( GameDifficulty difficulty)
        {
            Console.Clear();
            RunProblem('*', difficulty);
        }

        internal void RunProblem(char operation, GameDifficulty difficulty)
        {
            int score = 0;
            int attempts = 5;
            int result = 0;
            int Addend1;
            int Addend2;

            for (int i = 0; i < attempts; i++)
            {
                Console.Clear();
                helpers.TypeString($"Score: {score} Attempts Left: {attempts - (i)}");

                Addend1 = Helpers.GetRandomInt(difficulty);
                Addend2 = Helpers.GetRandomInt(difficulty);

                helpers.TypeString($"Question {i + 1}: " + Addend1 + $" {operation} " + Addend2 + " = ");

                switch (operation) 
                {
                    case '+':
                        result = Addend1 + Addend2; 
                        break;
                    case '-':
                        result = Addend1 - Addend2;
                        while (Addend2 < Addend1)
                        {
                            Addend2 = Helpers.GetRandomInt(difficulty);
                        }
                        break;
                    case '*':
                        result = Addend1 * Addend2;
                        break;
                    case '/':
                        while (Addend2 == 0 || Addend1 % Addend2 != 0)
                        {
                            Addend2 = Helpers.GetRandomInt(difficulty);
                        }
                        result = Addend1 / Addend2;
                        break;
                } 
                var answer = Helpers.UserIntInput();

                while (!Helpers.CheckAnswer(result, answer))
                {
                    Console.Clear();
                    attempts--;
                    helpers.TypeString($"Attempts Left: {attempts - (i)}");
                    helpers.TypeString($"Question {i + 1}: " + Addend1 + $" {operation} " + Addend2 + " = ");
                    answer = Helpers.UserIntInput();
                }

                score++;
                Helpers.HoldForEnter();
            }

            Console.WriteLine($"Your final score was: {score}");
            Helpers.HoldForEnter();

        }
    }
}
