using MySqlConnector;
using System;
using System.Globalization;
using System.Reflection;
using To_Do_App;
using BetterConsoleTables;

class Program
{
    static void Main(string[] args)
    {
        // Main menu
        while (true)
        {
            int choice = ChooseOptionByUser();
            Console.Clear();

            if (choice == 0)
            {
                ListTodosToday();
            }
            else if (choice == 1)
            {
                ListAllTodos();
            }
            else if (choice == 2)
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
                Console.WriteLine("Enter day (01 - 31:");
                Console.Write(">> ");
                string userDay = Console.ReadLine();

                Console.WriteLine("Enter month (01 - 12):");
                Console.Write(">> ");
                string userMonth = Console.ReadLine();

                Console.WriteLine("Enter year >= " + DateTime.Now.Year + ":");
                Console.Write(">> ");
                string userYear = Console.ReadLine();
                int userYearInt;
                try
                {
                    // Check year
                    userYearInt = Int32.Parse(userYear);
                    if (userYearInt < DateTime.Now.Year)
                    {
                        Console.WriteLine("Your input is wrong. The Year you entered is in the past");
                        continue;
                    }
                    else
                    {
                        newTodo.DateValue = userYearInt + "-" + userMonth + "-" + userDay;
                        Console.WriteLine("The date you entered is: " + newTodo.DateValue);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                

                // Safe To-Do in database
                try
                {
                    SafeToDatabase(newTodo.Title, newTodo.Description, newTodo.DateValue);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else if (choice == 3)
            {
                string connString = "Server=localhost;Database=todoapp;Uid=root;Pwd=;AllowZeroDateTime=true;";
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT * FROM todos";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    Table table = new Table("Task ID", "Title", "Description", "Expire date");
                    while (rdr.Read())
                    {
                        table.AddRow(rdr[0], rdr[1], rdr[2], rdr[3]);
                    }
                    rdr.Close();
                    Console.Write(table.ToString());

                    try
                    {
                        // Ask user which tasks are finished
                        Console.WriteLine("Which task have you done? Select by 'Task ID' (Only one at a time).");
                        Console.Write(">> ");
                        string finishedTasks = Console.ReadLine();

                        //Test if the input is a number
                        try
                        {
                            int test_input = Int32.Parse(finishedTasks);
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                        string query_delete = $"DELETE FROM todos WHERE id = {finishedTasks};";
                        MySqlCommand cmd_delete = new MySqlCommand(query_delete, conn);
                        cmd_delete.ExecuteNonQuery();

                        Console.WriteLine("Successfully completed task(s) " + finishedTasks);
                        Console.WriteLine("Press any key for showing all todos.");
                        Console.ReadLine();

                        MySqlDataReader rdr_updated = cmd.ExecuteReader();
                        Table table_updated = new Table("Task ID", "Title", "Description", "Expire date");
                        while (rdr_updated.Read())
                        {
                            table_updated.AddRow(rdr_updated[0], rdr_updated[1], rdr_updated[2], rdr_updated[3]);
                        }
                        rdr_updated.Close();
                        Console.Write(table_updated.ToString());
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    


                    conn.Close();
                }

                
            }
            else if (choice == 4)
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Please check your input. It is only possible to enter Numbers from 1 to 5");
                continue;
            }

            Console.WriteLine("\nPress any key for menu.");
            Console.Write(">> ");
            Console.ReadLine();
        }
        
    }

    static int ChooseOptionByUser()
    {
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

        return selectedOption;
    }

    static void ListTodosToday()
    {
        string connString = "Server=localhost;Database=todoapp;Uid=root;Pwd=;AllowZeroDateTime=true;";
        using (MySqlConnection conn = new MySqlConnection(connString))
        {
            conn.Open();
            string query = $"SELECT * FROM todos WHERE date = '{DateTime.Now.ToString("yyyy-MM-dd")}';";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            Table table = new Table("Task ID", "Title", "Description", "Expire date");
            while (rdr.Read())
            {
                table.AddRow(rdr[0], rdr[1], rdr[2], rdr[3]);
            }
            rdr.Close();
            conn.Close();
            Console.Write(table.ToString());
        }
    }

    static void ListAllTodos()
    {
        string connString = "Server=localhost;Database=todoapp;Uid=root;Pwd=;AllowZeroDateTime=true;";
        using (MySqlConnection conn = new MySqlConnection(connString))
        {
            conn.Open();
            string query = "SELECT * FROM todos";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            Table table = new Table("Task ID", "Title", "Description", "Expire date");
            while (rdr.Read())
            {
                table.AddRow(rdr[0], rdr[1], rdr[2], rdr[3]);
            }
            rdr.Close();
            conn.Close();
            Console.Write(table.ToString());
        }
    }

    static void SafeToDatabase(string title, string description, string dateValue)
    {
        string connString = "Server=localhost;Database=todoapp;Uid=root;Pwd=;";
        using (MySqlConnection conn = new MySqlConnection(connString))
        {
            conn.Open();
            string query = "INSERT INTO todos (title, description, date) VALUES (@val1, @val2, @val3)";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@val1", title);
                cmd.Parameters.AddWithValue("@val2", description);
                cmd.Parameters.AddWithValue("@val3", dateValue);
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        Console.WriteLine("The task was succsessfully saved.");
    }
}