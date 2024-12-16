using System.ComponentModel;
using CodingLogger;
using Spectre.Console;
using static CodingLogger.Enums;

internal class UserInterface
{
    internal void Menu()
    {
        while (true)
        {
            Console.Clear();

            var MenuOptions = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenu>()
                .Title("What do you want to do next?")
                .AddChoices(Enum.GetValues<MainMenu>()));


            switch (MenuOptions)
            {
                case MainMenu.StartSession:
                    AnsiConsole.Write("StartingSession");
                    SessionController newSession = new SessionController();
                    newSession.StartSession();
                    break;
                case MainMenu.ViewPreviousSessions:
                    AnsiConsole.Write("Finding Previous Sessions");
                    SessionsViewer ViewSession = new SessionsViewer();
                    ViewSession.DisplaySessionsTable();
                    break;
                case MainMenu.AddSession:
                    ManualAdjustSessions ManualNewSession = new ManualAdjustSessions();
                    ManualNewSession.InitialiseAddSession();
                    break;
            }
        }
    }
}