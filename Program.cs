using System;
using System.Globalization;
using System.Reflection;
using To_Do_App;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1. List all To-Dos for today.\n" +
                "2. List all To-Dos that you have.\n" +
                "3. Add new To-Do.\n" +
                "4. Exit.\n");
            string choice = Console.ReadLine();

            if (choice == "1")
            {

            }
            else if (choice == "2")
            {
            
            }
            else if (choice == "3")
            {
                // Title
                Console.WriteLine("Enter title...");
                Todo newTodo= new Todo();
                newTodo.Title = Console.ReadLine();

                // Description
                Console.WriteLine("Enter description...");
                newTodo.Description = Console.ReadLine();

                // Date
                Console.WriteLine("Enter date...");
                string dateInput = Console.ReadLine();

                string pattern = "dd-MM-yy";
                DateTime parsedDate;

                if (DateTime.TryParseExact(dateInput, pattern, null, DateTimeStyles.None, out parsedDate))
                {

                }
                else
                {
                    Console.WriteLine("Error: Unable to convert '{0}' to a date and time.",
                        dateInput);
                }

                newTodo.DateValue = parsedDate;
                    
                                        
            }
            else if (choice == "4")
            {

            }
            else
            {
                Console.WriteLine("Please check your input. It is only possible to enter Numbers from 1 to 4");
                continue;
            }
        }
        
    }
}