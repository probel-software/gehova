using Probel.Gehova.Business.Models;
using System;
using System.Collections.Generic;

namespace Probel.Gehova.Cli.Helpers
{
    public static class Output
    {
        #region Fields

        private const int LINE_SIZE = 80;
        private const char TITLE_BAR = '=';

        #endregion Fields

        #region Methods

        public static void Write(TeamModel model)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("        Team: ");
            Console.ResetColor();

            if (model == null) { Console.WriteLine("NULL"); }
            else { Console.WriteLine($"[{model.Id}] {model.Name}"); }
        }

        public static void Write(PersonCategoryModel model)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("    Category: ");
            Console.ResetColor();

            if (model == null) { Console.WriteLine("NULL"); }
            else { Console.WriteLine($"[{model.Id}] {model.Display} ({model.Key})"); }
        }

        public static void Write(PickupRoundModel model)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Pickup round: ");
            Console.ResetColor();

            if (model == null) { Console.WriteLine("NULL"); }
            else { Console.WriteLine($"[{model.Id}] {model.Name}"); }
        }

        public static void Write(PersonDisplayModel model)
        {
            WriteLine($"[{model.Id,-2}] - {model.FirstName} {model.LastName}");
            WriteLine($"     - Categories         : {model.Category}");
            WriteLine($"     - Team               : {model.Team}.");
            WriteLine($"     - IsLunchTime        : {model.IsLunchTime}");
            WriteLine($"     - IsReceptionMorning : {model.IsReceptionMorning}");
            WriteLine($"     - IsReceptionEvening : {model.IsReceptionEvening}");

        }

        public static void Write(IEnumerable<PersonDisplayModel> models)
        {
            foreach (var model in models) { Write(model); }
        }

        public static void WriteBigTitle(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();

            var separator = string.Empty;
            for (var i = 0; i < LINE_SIZE; i++) { separator += TITLE_BAR; }

            var prefix = string.Empty;
            for (var i = 0; i < 3; i++) { prefix += TITLE_BAR; }

            Console.WriteLine(separator);
            Console.WriteLine(prefix + " " + msg);
            Console.WriteLine(separator);
            Console.ResetColor();
        }

        public static void WriteDebug(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        public static void WriteLine(string message) => Console.WriteLine(message);

        public static void WriteTitle(string msg)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(msg);

            var separator = string.Empty;
            for (var i = 0; i < msg.Length; i++) { separator += '-'; }

            Console.WriteLine(separator);
            Console.ResetColor();
        }

        #endregion Methods
    }
}