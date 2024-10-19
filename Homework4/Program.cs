using System;

namespace Homework4
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseConnection.InitializeDatabase();
            var operations = new CrudOperations();

            while (true)
            {
                Console.WriteLine("\nSelect an action:");
                Console.WriteLine("1. Create a new document");
                Console.WriteLine("2. Read documents");
                Console.WriteLine("3. Update a document");
                Console.WriteLine("4. Delete a document");
                Console.WriteLine("5. Exit");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        operations.CreateDocument();
                        break;
                    case "2":
                        operations.ReadDocuments();
                        break;
                    case "3":
                        operations.UpdateDocument();
                        break;
                    case "4":
                        operations.DeleteDocument();
                        break;
                    case "5":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }
    }
}
