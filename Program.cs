using System;

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