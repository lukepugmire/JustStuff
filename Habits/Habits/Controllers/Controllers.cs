using System.Globalization;
using Microsoft.Data.Sqlite;

namespace Habits
{
    public class Habits
    {

        internal static List<string> GetAllHabits()
        {
            var tableNames = new List<string>();

            using (var connection = new SqliteConnection(DbHelper.connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table';";

                using (var reader = tableCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tableNames.Add(reader.GetString(0));
                    }
                }
            }
            return tableNames;
        }
        public static bool GetAllData(string habit)
        {
            bool hasData = false;
            Console.Clear();
            List<sleepObject> sleepsList = new List<sleepObject>();

            using (var connection = new SqliteConnection(DbHelper.connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"SELECT * FROM {habit}";


                SqliteDataReader reader = tableCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        hasData = true;
                        sleepsList.Add(new sleepObject
                        {
                            Id = reader.GetInt32(0),
                            Date = DateTime.ParseExact(reader.GetString(1), "dd-MM-yyyy", new CultureInfo("en-GB")),
                            Quantity = reader.GetInt32(2),
                        });
                    }
                }
                else
                {
                    Console.WriteLine("No Data Found");
                }
                connection.Close();
            }

            foreach (var SleepItem in sleepsList)
            {
                Console.WriteLine($"{SleepItem.Id} - {SleepItem.Date.ToString("dd-MM-yyyy")} - Quantity: {SleepItem.Quantity}");
            }
            Console.WriteLine("------------------------------------------\n");
            return hasData;
        }

        public static void Insert(string habit)
        {
            Console.Clear();
            float quantity = Userinputs.GetNumberInput("\n\nPlease type the desired Quantity. Type 0 to return to main manu.\n\n");
            string date = Userinputs.GetDateTimeInput("\n\nPlease insert the date as: dd-mm-yyyy. Type 0 to return to main manu.\n\n");
            using (var connection = new SqliteConnection(DbHelper.connectionString))
            {
                connection.Open();

                var quantityCmd = connection.CreateCommand();
                quantityCmd.CommandText = $"PRAGMA table_info({habit});";
                using (var columnReader = quantityCmd.ExecuteReader())
                {
                    string quantityColumn = null;

                    while (columnReader.Read())
                    {
                        string columnName = columnReader.GetString(1);

                        if (columnName != "Id" && columnName != "Date")
                        {
                            quantityColumn = columnName;
                            break;
                        }
                    }
                    using (var tableCmd = connection.CreateCommand())
                    {

                        tableCmd.CommandText = $"INSERT INTO {habit}(Date, {quantityColumn}) VALUES(@date, @quantity)";

                        tableCmd.Parameters.AddWithValue("@date", date);
                        tableCmd.Parameters.AddWithValue("@quantity", quantity);
                        tableCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void Update(string habit)
        {
            Console.Clear();
            bool HasData = GetAllData(habit);
            if (HasData == false)
            {
                Console.WriteLine("No data present, press any key to return to the menu");
                Console.ReadKey();
                Userinputs.GetUserInput();
            }
            bool isUpdating = true;


            using (var connection = new SqliteConnection(DbHelper.connectionString))
            {

                connection.Open();

                while (isUpdating)
                {
                    var recordId = Userinputs.GetNumberInput("\n\nPlease type Id of the record would like to update.\n\n", false);

                    while (recordId != Convert.ToInt32(recordId))
                    {
                        recordId = Userinputs.GetNumberInput("\n\nPlease type Id of the record would like to update.\n\n", false);
                    }
                    var checkCmd = connection.CreateCommand();
                    checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM {habit} WHERE Id = {recordId})";
                    int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (checkQuery == 0)
                    {
                        Console.WriteLine($"\n\nRecord with Id {recordId} doesn't exist.\n\n");
                        connection.Close();
                        Update(habit);
                    }

                    float quantity = Userinputs.GetNumberInput("\n\nPlease update with the correct Quantity.\n\n", false);
                    string date = Userinputs.GetDateTimeInput("\n\nPlease update with the correct date as: dd-mm-yyyy.\n\n", false);


                    var quantityCmd = connection.CreateCommand();
                    quantityCmd.CommandText = $"PRAGMA table_info({habit});";
                    using (var columnReader = quantityCmd.ExecuteReader())
                    {
                        string quantityColumn = null;

                        while (columnReader.Read())
                        {
                            string columnName = columnReader.GetString(1);

                            if (columnName != "Id" && columnName != "Date")
                            {
                                quantityColumn = columnName;
                                break;
                            }
                        }

                        var tableCmd = connection.CreateCommand();
                        tableCmd.CommandText = $"UPDATE {habit} SET Date = @date, {quantityColumn} = @quantity WHERE Id = @Id";
                        tableCmd.Parameters.AddWithValue("@Id", recordId);
                        tableCmd.Parameters.AddWithValue("@date", date);
                        tableCmd.Parameters.AddWithValue("@quantity", quantity);

                        tableCmd.ExecuteNonQuery();

                        Console.Clear();
                        GetAllData(habit);

                        Console.WriteLine("Press 0 if you are finished updating, else press any other key to continue");
                        var finishUpdating = Console.ReadLine();
                        if (finishUpdating == "0")
                        {
                            isUpdating = false;
                        }
                    }
                }
            }
        }
        internal static void Delete(string habit)
        {
            Console.Clear();
            GetAllData(habit);
            bool isDeleting = true;

            using (var connection = new SqliteConnection(DbHelper.connectionString))
            {
                connection.Open();
                while (isDeleting)
                {
                    var recordId = Userinputs.GetNumberInput("\n\nPlease type the Id of the record you want to delete.\n\n", false);
                    var tableCmd = connection.CreateCommand();
                    tableCmd.CommandText = $"DELETE FROM {habit} WHERE Id = @Id";
                    tableCmd.Parameters.AddWithValue("@Id", recordId);

                    int rowCount = tableCmd.ExecuteNonQuery();

                    if (rowCount == 0)
                    {
                        Console.WriteLine($"\n\nRecord with Id {recordId} doesn't exist. \n\n");
                        Delete(habit);
                    }

                    Console.WriteLine($"\n\nRecord with Id {recordId} was deleted. \n\n");

                    GetAllData(habit);
                    Console.WriteLine("Press 0 if you are finished deleting records, else press any other key to continue");
                    var finishDeleting = Console.ReadLine();
                    if (finishDeleting == "0")
                    {
                        isDeleting = false;
                    }
                }
            }
        }
    }
}