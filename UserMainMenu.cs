using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace To_Do_App
{
    class UserMainMenu
    {
        public string MainMenu()
        {
            string choice;

            string[] options = { "1. List all To-Dos for today.",
                "2. List all To-Dos that you have.",
                "3. Add new To-Do.",
                "4. Mark ToDos as finished",
                "5. Exit."
            };
            int selectedOption = 0;
            ConsoleKeyInfo key;

            do
            {
                Console.Clear();
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(options[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(options[i]);
                    }
                }

                key = Console.ReadKey();

                if (key.Key == ConsoleKey.UpArrow)
                {
                    selectedOption--;
                    if (selectedOption < 0)
                    {
                        selectedOption = options.Length - 1;
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    selectedOption++;
                    if (selectedOption == options.Length)
                    {
                        selectedOption = 0;
                    }
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine("You selected " + options[selectedOption]);
            Console.ReadKey();

            return selectedOption.ToString(); ;
        }
    }
}
