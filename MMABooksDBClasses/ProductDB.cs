using MMABooksBusinessClasses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace MMABooksDBClasses
{
    public static class ProductDB
    {
        public static Product GetProduct(string productCode)
        {
            using (MySqlConnection connection = MMABooksDB.GetConnection())
            {
                string selectStatement = "SELECT ProductCode, Description, UnitPrice, OnHandQuantity FROM Products WHERE ProductCode = @ProductCode";
                MySqlCommand selectCommand = new MySqlCommand(selectStatement, connection);
                selectCommand.Parameters.AddWithValue("@ProductCode", productCode);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                    if (reader.Read())
                    {
                        return new Product
                        {
                            ProductCode = reader["ProductCode"].ToString(),
                            Description = reader["Description"].ToString(),
                            UnitPrice = (decimal)reader["UnitPrice"],
                            OnHandQuantity = (int)reader["OnHandQuantity"]
                        };
                    }
                    return null;
                }
                catch (MySqlException ex)
                {
                    throw ex;
                }
            }
        }

        public static int AddProduct(Product product)
        {
            using (MySqlConnection connection = MMABooksDB.GetConnection())
            {
                string insertStatement = "INSERT INTO Products (ProductCode, Description, UnitPrice, OnHandQuantity) " +
                    "VALUES (@ProductCode, @Description, @UnitPrice, @OnHandQuantity)";
                MySqlCommand insertCommand = new MySqlCommand(insertStatement, connection);
                insertCommand.Parameters.AddWithValue("@ProductCode", product.ProductCode);
                insertCommand.Parameters.AddWithValue("@Description", product.Description);
                insertCommand.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                insertCommand.Parameters.AddWithValue("@OnHandQuantity", product.OnHandQuantity);

                try
                {
                    connection.Open();
                    insertCommand.ExecuteNonQuery();
                    return (int)insertCommand.LastInsertedId; // This will return 0 as there's no auto-increment ID
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error adding product: " + ex.Message);
                    throw;
                }
            }
        }

        public static bool DeleteProduct(Product product)
        {
            using (MySqlConnection connection = MMABooksDB.GetConnection())
            {
                string deleteStatement = "DELETE FROM Products WHERE ProductCode = @ProductCode";
                MySqlCommand deleteCommand = new MySqlCommand(deleteStatement, connection);
                deleteCommand.Parameters.AddWithValue("@ProductCode", product.ProductCode);

                try
                {
                    connection.Open();
                    int rowsAffected = deleteCommand.ExecuteNonQuery();
                    return rowsAffected == 1;
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1451)
                    {
                        Console.WriteLine("Cannot delete product because it is referenced in an invoice line item.");
                        return false;
                    }
                    throw ex;
                }
            }
        }

        public static bool UpdateProduct(Product oldProduct, Product newProduct)
        {
            using (MySqlConnection connection = MMABooksDB.GetConnection())
            {
                string updateStatement = "UPDATE Products SET Description = @NewDescription, UnitPrice " +
                    "= @NewUnitPrice, OnHandQuantity = @NewOnHandQuantity WHERE ProductCode = @OldProductCode";
                MySqlCommand updateCommand = new MySqlCommand(updateStatement, connection);
                updateCommand.Parameters.AddWithValue("@NewDescription", newProduct.Description);
                updateCommand.Parameters.AddWithValue("@NewUnitPrice", newProduct.UnitPrice);
                updateCommand.Parameters.AddWithValue("@NewOnHandQuantity", newProduct.OnHandQuantity);
                updateCommand.Parameters.AddWithValue("@OldProductCode", oldProduct.ProductCode);

                try
                {
                    connection.Open();
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    return rowsAffected == 1;
                }
                catch (MySqlException ex)
                {
                    throw ex;
                }
            }
        }
    }
}