using Spectre.Console;
using Dapper;
namespace CodingLogger;

internal class SessionController
{
    private volatile bool isRunning = true;
    private volatile bool isPaused = false;
    private int elapsedSeconds = 0;
    private string? StartTime;
    private string? EndTime;

    internal void StartSession()
    {

        Thread clockThread = new Thread(DisplayClock);
        clockThread.Start();

        HandleUserInput();
        clockThread.Join();
        EndTime = DateTime.Now.ToString();

        AnsiConsole.Clear();
        AnsiConsole.WriteLine("Session Ended");
        AnsiConsole.WriteLine($"Start Time: {StartTime}");
        AnsiConsole.WriteLine($"End Time: {EndTime}");
        AnsiConsole.WriteLine($"Duration: {elapsedSeconds} seconds \n\n");
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
        };




        AnsiConsole.Ask<string>("Press [yellow]Enter[/] to continue...");

    }

    private void DisplayClock()
    {
        DateTime lastUpdateTime = DateTime.Now;
        StartTime = lastUpdateTime.ToString();
        AnsiConsole.WriteLine("Press 'P' to Pause, 'R' to Resume, or 'S' to Stop.");
        AnsiConsole.Live(new Panel("Clock")).Start(ctx =>
                    {
                        while (isRunning)
                        {
                            if (!isPaused)
                            {

                                DateTime now = DateTime.Now;
                                elapsedSeconds += (int)(now - lastUpdateTime).TotalSeconds;
                                lastUpdateTime = now;
                                TimeSpan elapsedTime = TimeSpan.FromSeconds(elapsedSeconds);

                                ctx.UpdateTarget(new Panel($"[bold green]Clock:[/] [bold green]{DateTime.Now:HH:mm:ss}[/]")
                           .Padding(6, 0)
                           .BorderColor(Color.White)
                           .Border(BoxBorder.Ascii));

                            }
                            else
                            {
                                // Update lastUpdateTime when paused to prevent accumulation
                                lastUpdateTime = DateTime.Now;
                            }

                            Thread.Sleep(1000);
                        }
                    });
    }

    private void HandleUserInput()
    {
        int Pauses = 0;
        while (isRunning)
        {
            var key = Console.ReadKey(intercept: true).Key;

            switch (key)
            {
                case ConsoleKey.P:
                    isPaused = true;
                    Pauses++;
                    break;
                case ConsoleKey.R:
                    isPaused = false;
                    break;
                case ConsoleKey.S:
                    isRunning = false;
                    break;
            }
        }
    }
}



