using MySql.Data.MySqlClient;
using System;

namespace Homework4
{
    public class CrudOperations
    {
        public void CreateDocument()
        {
            try
            {
                string name = InputValidation.GetValidInput("Enter name: ", InputValidation.ValidateName);
                string email = InputValidation.GetValidInput("Enter email: ", InputValidation.ValidateEmail);
                string phone = InputValidation.GetValidInput("Enter phone number: ", InputValidation.ValidatePhone);
                string address = InputValidation.GetValidInput("Enter address: ", InputValidation.ValidateAddress);

                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    var command = new MySqlCommand($@"
                        INSERT INTO {DatabaseConnection.TableName} (name, email, phone, address, created_at)
                        VALUES (@name, @name_email, @phone, @address, CURRENT_TIMESTAMP);", connection);

                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@name_email", email);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@address", address);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Document created.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while creating the document: " + ex.Message);
            }
        }

        public void ReadDocuments()
        {
            try
            {
                string filterField = GetInput("Enter filter type (name/email/phone) or press Enter to read all: ");
                string filterValue = string.IsNullOrWhiteSpace(filterField) ? "" : GetInput($"Enter the value to filter {filterField} by: ");

                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    string query = $"SELECT * FROM {DatabaseConnection.TableName}";
                    if (!string.IsNullOrWhiteSpace(filterField) && !string.IsNullOrWhiteSpace(filterValue))
                    {
                        query += $" WHERE {filterField} = @filterValue";
                    }

                    var command = new MySqlCommand(query, connection);
                    if (!string.IsNullOrWhiteSpace(filterField) && !string.IsNullOrWhiteSpace(filterValue))
                    {
                        command.Parameters.AddWithValue("@filterValue", filterValue);
                    }

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Name: {reader["name"]}, Email: {reader["email"]}, Phone: {reader["phone"]}, Address: {reader["address"]}, Created At: {reader["created_at"]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while reading documents: " + ex.Message);
            }
        }

        public void UpdateDocument()
        {
            try
            {
                string id = GetInput("Enter the ID of the document you wish to update: ");

                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    var getCommand = new MySqlCommand($"SELECT * FROM {DatabaseConnection.TableName} WHERE id = @id;", connection);
                    getCommand.Parameters.AddWithValue("@id", id);
                    var reader = getCommand.ExecuteReader();

                    if (!reader.Read())
                    {
                        Console.WriteLine("Document not found.");
                        return;
                    }

                    string currentEmail = reader["email"].ToString();
                    string currentPhone = reader["phone"].ToString();
                    string currentAddress = reader["address"].ToString();

                    reader.Close();

                    string newEmail = GetInput($"Enter new email (Enter to skip: {currentEmail}): ");
                    string newPhone = GetInput($"Enter new phone (Enter to skip: {currentPhone}): ");
                    string newAddress = GetInput($"Enter new address (Enter to skip: {currentAddress}): ");

                    newEmail = string.IsNullOrWhiteSpace(newEmail) ? currentEmail : newEmail;
                    newPhone = string.IsNullOrWhiteSpace(newPhone) ? currentPhone : newPhone;
                    newAddress = string.IsNullOrWhiteSpace(newAddress) ? currentAddress : newAddress;

                    var updateCommand = new MySqlCommand($@"
                        UPDATE {DatabaseConnection.TableName}
                        SET email = @newEmail, phone = @newPhone, address = @newAddress
                        WHERE id = @id;", connection);

                    updateCommand.Parameters.AddWithValue("@newEmail", newEmail);
                    updateCommand.Parameters.AddWithValue("@newPhone", newPhone);
                    updateCommand.Parameters.AddWithValue("@newAddress", newAddress);
                    updateCommand.Parameters.AddWithValue("@id", id);

                    updateCommand.ExecuteNonQuery();
                    Console.WriteLine("Document updated.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating the document: " + ex.Message);
            }
        }

        public void DeleteDocument()
        {
            try
            {
                string id = GetInput("Enter the ID of the document you want to delete: ");

                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();
                    var command = new MySqlCommand($"DELETE FROM {DatabaseConnection.TableName} WHERE id = @id;", connection);
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Document deleted.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while deleting the document: " + ex.Message);
            }
        }

        private string GetInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
