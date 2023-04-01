﻿using MySqlConnector;
using System;
using System.Globalization;
using System.Reflection;
using To_Do_App;
using BetterConsoleTables;

class Program
{
    static void Main(string[] args)
    {
        const string pattern = "dd-MM-yy";

        // Main menu
        while (true)
        {
            Console.WriteLine("==============================================="); 
            Console.WriteLine("1. List all To-Dos for today.\n" +
                "2. List all To-Dos that you have.\n" +
                "3. Add new To-Do.\n" +
                "4. Mark ToDos as finished\n" +
                "5. Exit.");
            Console.Write(">> ");
            string choice = Console.ReadLine();
            Console.WriteLine("===============================================");

            if (choice == "1")
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
            else if (choice == "2")
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
                    string connString = "Server=localhost;Database=todoapp;Uid=root;Pwd=;";
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    {
                        conn.Open();
                        string query = "INSERT INTO todos (title, description, date) VALUES (@val1, @val2, @val3)";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@val1", newTodo.Title);
                            cmd.Parameters.AddWithValue("@val2", newTodo.Description);
                            cmd.Parameters.AddWithValue("@val3", newTodo.DateValue);
                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();
                    }
                    Console.WriteLine("The task was succsessfully saved.");
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else if (choice == "4")
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
            else if (choice == "5")
            {
                Environment.Exit(0);
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