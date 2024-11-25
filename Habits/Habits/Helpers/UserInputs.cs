namespace Habits
{
    class Userinputs
    {


        internal static void SelectHabit()
        {
            Console.WriteLine("Select a Habit:");
            List<string> habits = Habits.GetAllHabits();
            for (int i = 0; i < habits.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {habits[i]}");
            }
            Console.WriteLine("Enter the number corresponding to your choice:");


            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= habits.Count)
            {
                string selectedHabit = habits[choice - 1];
                Console.WriteLine($"You selected: {selectedHabit}");

                GetUserInput(selectedHabit);
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }

        }
        internal static void GetUserInput(string habit = "sleep_regularity")
        {

            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine($"\n\nMenu - Chose habit: {habit}");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine(@"Type the corrosponding number?
                0 - return to Main Menu
                1 - View all
                2 - Insert
                3 - Update
                4 - Delete ");

                string userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "0":
                        isRunning = false;
                        break;
                    case "1":
                        Habits.GetAllData(habit);
                        Console.WriteLine("\nPress any key to return to the menu...");
                        Console.ReadKey();
                        break;
                    case "2":
                        Habits.Insert(habit);
                        break;
                    case "3":
                        Habits.Update(habit);
                        break;
                    case "4":
                        Habits.Delete(habit);
                        break;
                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4. \n");
                        break;
                }
            }
        }
        internal static float GetNumberInput(string message, bool menuOption = true)
        {

            while (true)
            {
                Console.WriteLine(message);
                string input = Console.ReadLine();
                if (menuOption == true)
                {
                    if (input == "0") GetUserInput();
                }
                if (float.TryParse(input, out float Output))
                {
                    return Output;
                }
                Console.WriteLine("Invalid Input, Please enter a valid float");
            }
        }

        internal static string GetDateTimeInput(string message, bool menuOption = true)
        {
            Console.WriteLine(message);
            string? dateInput = Console.ReadLine();
            if (menuOption == true)
            {
                if (dateInput == "0") GetUserInput();
            }
            return dateInput;
        }
    }
}