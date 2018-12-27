using Probel.Gehova.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Probel.Gehova.Cli.Helpers
{
    public static class Output
    {
        #region Fields

        private const int LINE_SIZE = 80;

        private const char TITLE_BAR = '=';

        #endregion Fields

        #region Methods

        internal static void WriteError(string message)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press <ENTER> to sontinue...");
            Console.ReadLine();
        }

        public static void Write(TeamDisplayModel model)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("        Team: ");
            Console.ResetColor();

            if (model == null) { Console.WriteLine("NULL"); }
            else { Console.WriteLine($"[{model.Id}] {model.Name}"); }
        }

        public static void Write(TeamModel model)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Team: ");
            Console.ResetColor();

            if (model == null) { Console.WriteLine("NULL"); }
            else
            {
                Console.WriteLine($"[{model.Id}] {model.Name}");

                if (model.People != null && model.People.Count() > 0)
                {
                    foreach (var person in model.People)
                    {
                        Console.Write("\t");
                        Write(person);
                    }
                }
                else { WriteLine("\tNobody."); }
            }
        }

        public static void Write(AbsenceModel model)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"Absence for {model.Person.FirstName} {model.Person.LastName}");
            Console.ResetColor();

            if (model == null) { Console.WriteLine("NULL"); }
            else
            {
                Console.WriteLine($"   - From: {model.From}");
                Console.WriteLine($"   - To  : {model.To}");
                Console.WriteLine($"   - Id  : {model.Id}");
            }
        }

        public static void Write(CategoryModel model)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("    Category: ");
            Console.ResetColor();

            if (model == null) { Console.WriteLine("NULL"); }
            else { Console.WriteLine($"[{model.Id}] {model.Display} ({model.Key})"); }
        }

        public static void Write(PersonDisplayModel model)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Person: ");
            Console.ResetColor();

            if (model == null) { Console.WriteLine("NULL"); }
            else { Console.WriteLine($"[{model.Id}] {model.CategoryKey,9} -  {model.FirstName,15} {model.LastName,-15} -- {model.Category}"); }
        }

        public static void Write(PickupRoundDisplayModel model)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Pickup round: ");
            Console.ResetColor();

            if (model == null) { Console.WriteLine("NULL"); }
            else { Console.WriteLine($"[{model.Id}] {model.Name}"); }
        }

        public static void Write(PickupRoundModel model)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Pickup round: ");
            Console.ResetColor();

            if (model == null) { Console.WriteLine("NULL"); }
            else
            {
                Console.WriteLine($"[{model.Id}] {model.Name}");

                if (model.People != null && model.People.Count() > 0)
                {
                    foreach (var person in model.People)
                    {
                        Console.Write("\t");
                        Write(person);
                    }
                }
                else { WriteLine("\tNobody."); }
            }
        }

        public static void Write(PersonFullDisplayModel model)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            WriteLine($"Person: [{model.Id,-2}] - {model.FirstName} {model.LastName}");
            Console.ResetColor();

            WriteLine($"     - Categories         : {model.Category}");
            WriteLine($"     - Team               : {model.Team}.");
            WriteLine($"     - IsLunchTime        : {model.IsLunchTime}");
            WriteLine($"     - IsReceptionMorning : {model.IsReceptionMorning}");
            WriteLine($"     - IsReceptionEvening : {model.IsReceptionEvening}");
        }

        public static void Write(IEnumerable<PersonFullDisplayModel> models)
        {
            foreach (var model in models) { Write(model); }
        }

        public static void Write(IEnumerable<AbsenceDisplayModel> models)
        {
            foreach (var model in models) { Write(model); }
        }

        public static void Write(AbsenceDisplayModel model)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"Absence for [{model.PersonId}] {model.FirstName} {model.LastName}");
            Console.ResetColor();

            if (model == null) { Console.WriteLine("NULL"); }
            else
            {
                Console.WriteLine($"   - From: {model.From}");
                Console.WriteLine($"   - To  : {model.To}");
                Console.WriteLine($"   - Id  : {model.Id}");
            }
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