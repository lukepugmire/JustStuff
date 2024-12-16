using Microsoft.Data.Sqlite;
using Spectre.Console;
using Dapper;
using Microsoft.Extensions.Configuration;
using CodingLogger;


class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        DbConnectionService.initialize(configuration);

        initialiseDb CreateCodingLogger = new initialiseDb();
        CreateCodingLogger.InitialiseDbTable();

        UserInterface UI = new UserInterface();
        UI.Menu();

    }
}

/*
Goal = Logg daily coding time,
- display data on console
- data comes in "coding session" this will track: 
    - Id, StartTime, EndTime, Duration
    - (NOTE) Session should generally be initiated by user, 
    - User can input a sessions via startTime and EndTime,
*/