using Microsoft.Data.Sqlite;
using System.Text;

namespace Habits
{
    class HabitBuilder
    {

        internal static void ProgramInitialise()
        {

            using (var connection = new SqliteConnection(DbHelper.connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();
                var DeleteCmd = connection.CreateCommand();
                DeleteCmd.CommandText = "DROP TABLE sleep_regularity;";
                DeleteCmd.ExecuteNonQuery();

                tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS sleep_regularity
                                                                    (
                                                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                                    Date Text,
                                                                    Quantity INTERGER
                                                                    )";
                tableCmd.ExecuteNonQuery();

                PopulateHabits();



                bool isRunning = true;
                while (isRunning)
                {
                    Console.Clear();
                    Console.WriteLine("\n\nMain Menu");
                    Console.WriteLine("\nWhat would you like to do?");
                    Console.WriteLine(@"Type the corrosponding number?
                        0 - Close Application
                        1 - View a Habit
                        2 - Add a new Habit to track ");

                    string userChoice = Console.ReadLine();
                    string? Habit;

                    switch (userChoice)
                    {
                        case "0":
                            Console.WriteLine("\nGoodbye\n");
                            isRunning = false;
                            break;
                        case "1":
                            Console.WriteLine("Please Select the Habit you wish to view");

                            Userinputs.SelectHabit();
                            Habit = Console.ReadLine();
                            break;
                        case "2":
                            BuildHabits();
                            Console.WriteLine("Press any button to return to the Main Menu");
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4. \n");
                            break;
                    }
                }
            }
        }


        internal static void BuildHabits()
        {



            Console.Clear();
            Console.WriteLine(@"To build establish your own habit to track 
                                    you will need to supply a name for the habit   
                                    as well as a name for the quantity");
            string HabitName = "";
            string HabitQuantity = "";
            while (HabitName == "" || HabitQuantity == "")
            {
                Console.WriteLine("\n Write the name of your habit: ");
                HabitName = Console.ReadLine();
                Console.WriteLine("\n Write the name of the quantity you want to track it: ");
                HabitQuantity = Console.ReadLine();
            }



            using (var connection = new SqliteConnection(DbHelper.connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $@"CREATE TABLE IF NOT EXISTS {HabitName}
                                                                    (
                                                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                                    Date Text,
                                                                    {HabitQuantity} INTERGER
                                                                    )";


                try
                {
                    tableCmd.ExecuteNonQuery();
                    Console.WriteLine($"Table '{HabitName}' created successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while creating the table '{HabitName}': {ex.Message}");
                }
            }
        }



        internal static void PopulateHabits()
        {
            using (var connection = new SqliteConnection(DbHelper.connectionString))
            {
                connection.Open();
                using (SqliteCommand insertCmd = connection.CreateCommand())
                {
                    var sqlBuilder = new StringBuilder();
                    Random randomNumber = new Random();

                    for (int i = 0; i < 100; i++)
                    {
                        int percentRange = randomNumber.Next(0, 100);
                        string date = DateTime.Now.AddDays(-i).ToString("dd-MM-yyyy");
                        int? quantity;
                        if (percentRange < 75)
                        {
                            quantity = randomNumber.Next(5, 12);
                        }
                        else if (percentRange < 90)
                        {
                            quantity = randomNumber.Next(0, 5);
                        }
                        else if (percentRange < 99)
                        {
                            quantity = randomNumber.Next(12, 16);
                        }
                        else
                        {
                            quantity = randomNumber.Next(16, 24);
                        }

                        sqlBuilder.AppendLine($"INSERT INTO sleep_regularity (Date, Quantity) VALUES ('{date}', {quantity});");
                    }
                    insertCmd.CommandText = sqlBuilder.ToString();
                    try
                    {
                        insertCmd.ExecuteNonQuery();
                        Console.WriteLine("Successfully added 100 records into the 'sleep_regularity' table.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while populating the table: {ex.Message}");
                    }

                }
            }
        }
    }
}