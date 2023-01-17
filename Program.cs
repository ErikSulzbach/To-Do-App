using System;
using System.Globalization;
using System.Reflection;
using To_Do_App;

class Program
{
    static void Main(string[] args)
    {
        const string pattern = "dd-MM-yy";

        while (true)
        {
            Console.WriteLine("==============================================="); 
            Console.WriteLine("1. List all To-Dos for today.\n" +
                "2. List all To-Dos that you have.\n" +
                "3. Add new To-Do.\n" +
                "4. Exit.");
            Console.Write(">> ");
            string choice = Console.ReadLine();
            Console.WriteLine("===============================================");

            if (choice == "1")
            {

            }
            else if (choice == "2")
            {
            
            }
            else if (choice == "3")
            {
                // Title
                Console.WriteLine("Enter title:");
                Todo newTodo= new Todo();
                Console.Write(">> ");
                newTodo.Title = Console.ReadLine();

                // Description
                Console.WriteLine("Enter description:");
                Console.Write(">> ");
                newTodo.Description = Console.ReadLine();

                // Date
                //Console.WriteLine("Enter day as 'DD'...");
                //string day = Console.ReadLine();
                //Console.WriteLine("Enter month as 'MM'...");
                //string month = Console.ReadLine();
                //Console.WriteLine("Enter year as 'YYYY'...");
                //string year = Console.ReadLine();
                //string dateInput_ = day + " " + month + " " + year;
                //newTodo.DateValue = dateInput_;

                Console.WriteLine("Enter a date (dd/MM/yyyy): ");
                Console.Write(">> ");
                string dateInput = Console.ReadLine();
                try
                {
                    DateTime date = DateTime.Parse(dateInput);
                    string dateWithoutTime = date.ToString("dd/MM/yyyy");
                    Console.WriteLine("The date you entered is: " + dateWithoutTime);
                    newTodo.DateValue = dateWithoutTime;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else if (choice == "4")
            {

            }
            else
            {
                Console.WriteLine("Please check your input. It is only possible to enter Numbers from 1 to 4");
                continue;
            }

            Console.WriteLine("\nPress any key for menu.");
            Console.Write(">> ");
            Console.ReadLine();
        }
        
    }
}