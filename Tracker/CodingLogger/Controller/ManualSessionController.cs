using Spectre.Console;
using Dapper;
using static CodingLogger.Enums;

namespace CodingLogger;

internal class ManualAdjustSessions
{
    private int elapsedSeconds = 0;
    private string? StartTime;
    private string? EndTime;

    internal void InitialiseAddSession()
    {
        while (true)
        {
            var AddSessionOptions = AnsiConsole.Prompt(
                new SelectionPrompt<Adjust>()
                .Title("What do you want to do next?")
                .AddChoices(Enum.GetValues<Adjust>()));


            switch (AddSessionOptions)
            {
                case Adjust.Insert:
                    AddSession();
                    break;
                case Adjust.Delete:
                    DeleteSession();
                    break;
                case Adjust.Nothing:
                    return;
            }
        }
    }
    internal void DeleteSession()
    {
        int SessionId = AnsiConsole.Prompt(
            new TextPrompt<int>("Please enter a valid Id:")
            .Validate(Id => Id > 0
            ? ValidationResult.Success()
            : ValidationResult.Error("[red]Id must be a positive number.[/]")));

        using (var connection = DbConnectionService.GetConnection())
        {
            try
            {
                connection.Open();
                var RemoveSession = @"DELETE FROM _CodingSession WHERE id = @ID";
                var parameters = new
                {
                    ID = SessionId
                };
                int rowsAffected = connection.Execute(RemoveSession, parameters);

                if (rowsAffected > 0)
                {
                    AnsiConsole.WriteLine("Delete successful!");
                }
                else
                {
                    AnsiConsole.WriteLine("Delete executed, but no rows were affected.");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }
    internal void AddSession()
    {

        inputSessionValues();

        using (var connection = DbConnectionService.GetConnection())
        {
            try
            {
                connection.Open();
                var InsertSession = @"INSERT INTO _CodingSession (StartTime, EndTime, Duration ) VALUES(@SessionStartTime, @SessionEndTime, @Duration)";
                var parameters = new
                {
                    SessionStartTime = StartTime,
                    SessionEndTime = EndTime,
                    Duration = elapsedSeconds
                };
                int rowsAffected = connection.Execute(InsertSession, parameters);

                if (rowsAffected > 0)
                {
                    AnsiConsole.WriteLine("Insert successful!");
                }
                else
                {
                    AnsiConsole.WriteLine("Insert executed, but no rows were affected.");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }


    internal void inputSessionValues()
    {
        StartTime = AnsiConsole.Prompt(
            new TextPrompt<string>("Please add a valid start time (dd/MM/yyyy HH:mm:ss):")
            .Validate(text =>
            {
                return DateTime.TryParseExact
                (
                    text,
                    "dd/MM/yyyy HH:mm:ss",
                    null,
                    System.Globalization.DateTimeStyles.None,
                    out _)
                    ? ValidationResult.Success() : ValidationResult.Error("[red]Invalid date-time format. Please use dd/MM/yyyy HH:mm:ss.[/]");
            }));
        AnsiConsole.MarkupLine($"You entered a valid start sime: [green]{StartTime}[/]");

        EndTime = AnsiConsole.Prompt(
            new TextPrompt<string>("Please add a valid end time (dd/MM/yyyy HH:mm:ss):")
            .Validate(text =>
            {
                return DateTime.TryParseExact
                (
                    text,
                    "dd/MM/yyyy HH:mm:ss",
                    null,
                    System.Globalization.DateTimeStyles.None,
                    out _)
                    ? ValidationResult.Success() : ValidationResult.Error("[red]Invalid date-time format. Please use dd/MM/yyyy HH:mm:ss.[/]");
            }));
        AnsiConsole.MarkupLine($"You entered a valid end time: [green]{EndTime}[/]");

        elapsedSeconds = AnsiConsole.Prompt(
            new TextPrompt<int>("Please enter a valid duration in seconds:")
            .Validate(seconds => seconds > 0
            ? ValidationResult.Success()
            : ValidationResult.Error("[red]Duration must be a positive number.[/]")));
        AnsiConsole.MarkupLine($"You entered a duration of [green]{elapsedSeconds}[/] seconds.");
    }

}
