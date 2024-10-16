using MMABooksBusinessClasses;
using MMABooksDBClasses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMABooksTests
{
    [TestFixture]
    public class ProductTests
    {
        [SetUp]
        public void SetUp()
        {
            var existingProduct = ProductDB.GetProduct("A5CT");
            if (existingProduct != null)
            {
                bool deleted = ProductDB.DeleteProduct(existingProduct);
                Assert.IsTrue(deleted, "Failed to delete existing product.");
            }

            var product = new Product
            {
                ProductCode = "A5CT",
                Description = "Murach's ASP.NET 4 Web Programming with C# 2010",
                UnitPrice = 56.50m,
                OnHandQuantity = 4637
            };

          
            ProductDB.AddProduct(product);
        }

        [Test]
        public void TestGetProduct()
        {
            var product = ProductDB.GetProduct("A5CT");
            Assert.IsNotNull(product);
            Assert.AreEqual("A5CT", product.ProductCode);
            Assert.AreEqual("Murach's ASP.NET 4 Web Programming with C# 2010", product.Description);
            Assert.AreEqual(56.50m, product.UnitPrice);
            Assert.AreEqual(4637, product.OnHandQuantity);
        }

        [Test]
        public void TestUpdateProduct()
        {
            var oldProduct = ProductDB.GetProduct("A5CT");
            var newProduct = new Product
            {
                ProductCode = "A5CT",
                Description = "Updated Description",
                UnitPrice = 60.00m,
                OnHandQuantity = 5000
            };

            bool updated = ProductDB.UpdateProduct(oldProduct, newProduct);
            Assert.IsTrue(updated);

            var updatedProduct = ProductDB.GetProduct("A5CT");
            Assert.AreEqual("Updated Description", updatedProduct.Description);
            Assert.AreEqual(60.00m, updatedProduct.UnitPrice);
            Assert.AreEqual(5000, updatedProduct.OnHandQuantity);
        }

        [Test]
        public void TestDeleteProduct()
        {
            var product = ProductDB.GetProduct("A5CT");
            bool deleted = ProductDB.DeleteProduct(product);
            Assert.IsTrue(deleted);

            var deletedProduct = ProductDB.GetProduct("A5CT");
            Assert.IsNull(deletedProduct);
        }
    }
}