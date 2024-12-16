using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using Microsoft.Extensions.Configuration;

namespace CodingLogger;

internal class initialiseDb
{

    public void InitialiseDbTable()
    {
        using (var connection = DbConnectionService.GetConnection())
        {

            connection.Open();
            var createTable = @"CREATE TABLE IF NOT EXISTS _CodingSession 
                                                                    (
                                                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                                    StartTime TEXT,
                                                                    EndTime TEXT,
                                                                    Duration INTERGER
                                                                    )";

            try
            {
                var result = connection.Execute(createTable);
                if (result == 0)
                {
                    AnsiConsole.Write("The table already exists, no changes made.");
                }
                else
                {
                    AnsiConsole.Write("Table created successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                AnsiConsole.Write("An error occurred while creating the table: " + ex.ToString());
            }

        };
    }
}
