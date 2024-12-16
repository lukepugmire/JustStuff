using Spectre.Console;
using Dapper;
namespace CodingLogger;

internal class SessionsViewer
{
    internal void DisplaySessionsTable()
    {
        using (var connection = DbConnectionService.GetConnection())
        {
            connection.Open();
            var RetrieveSessions = @"SELECT * From  _CodingSession";
            var sessions = connection.Query<CodingSession>(RetrieveSessions);

            var table = new Table();

            table.AddColumn("ID");
            table.AddColumn("Date");
            table.AddColumn("Start Time");
            table.AddColumn("End Time");
            table.AddColumn("Duration");

            foreach (var session in sessions)
            {
                var startTime = DateTime.ParseExact(session.StartTime, "dd/MM/yyyy HH:mm:ss", null);
                var endTime = DateTime.ParseExact(session.EndTime, "dd/MM/yyyy HH:mm:ss", null);

                table.AddRow(
                    session.Id.ToString(),
                    startTime.ToString("dd/MM/yyyy"),
                    startTime.ToString("hh:mm tt"),
                    endTime.ToString("hh:mm tt"),
                    session.Duration.ToString()
                );
            }

            AnsiConsole.Clear();
            AnsiConsole.Write(table);
            Console.ReadLine();
        }
    }
}