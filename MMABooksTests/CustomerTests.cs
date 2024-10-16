using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using MMABooksBusinessClasses;
using MMABooksDBClasses;

namespace MMABooksTests
{
    [TestFixture]
    public class CustomerTests
    {
        private int testCustomerId;

        [SetUp]
        public void SetUp()
        {
            var existingCustomer = CustomerDB.GetCustomer(1);
            if (existingCustomer != null)
            {
            }

            var customer = new Customer
            {
                Name = "Molunguri, A",
                Address = "1108 Johanna Bay Drive",
                City = "Birmingham",
                State = "AL",
                ZipCode = "35216-6909"
            };

            testCustomerId = CustomerDB.AddCustomer(customer);
            Assert.Greater(testCustomerId, 0, "Customer ID should be greater than 0 after addition.");
        }

        [Test]
        public void TestGetCustomer()
        {
            var customer = CustomerDB.GetCustomer(testCustomerId);
            Assert.IsNotNull(customer, "Customer should not be null. Check if it was added successfully.");
            Assert.AreEqual("Molunguri, A", customer.Name);
            Assert.AreEqual("1108 Johanna Bay Drive", customer.Address);
            Assert.AreEqual("Birmingham", customer.City);
            Assert.AreEqual("AL", customer.State);
            Assert.AreEqual("35216-6909", customer.ZipCode.Trim());
        }

        [Test]
        public void TestUpdateCustomer()
        {
            var oldCustomer = CustomerDB.GetCustomer(testCustomerId);
            var newCustomer = new Customer
            {
                CustomerID = oldCustomer.CustomerID,
                Name = "Updated Name",
                Address = "Updated Address",
                City = "Updated City",
                State = "AL",
                ZipCode = "12345"
            };

            bool result = CustomerDB.UpdateCustomer(oldCustomer, newCustomer);
            Assert.IsTrue(result, "Update should succeed.");

            var updatedCustomer = CustomerDB.GetCustomer(testCustomerId);
            Assert.AreEqual("Updated Name", updatedCustomer.Name);
            Assert.AreEqual("Updated Address", updatedCustomer.Address);
            Assert.AreEqual("Updated City", updatedCustomer.City);
            Assert.AreEqual("AL", updatedCustomer.State);
            Assert.AreEqual("12345", updatedCustomer.ZipCode.Trim());
        }

        [Test]
        public void TestDeleteCustomer()
        {
            var customer = CustomerDB.GetCustomer(testCustomerId);
            bool result = CustomerDB.DeleteCustomer(customer);
            Assert.IsTrue(result, "Delete should succeed.");

            var deletedCustomer = CustomerDB.GetCustomer(testCustomerId);
            Assert.IsNull(deletedCustomer, "Customer should be null after deletion.");
        }

        [TearDown]
        public void TearDown()
        {
            var customer = CustomerDB.GetCustomer(testCustomerId);
            if (customer != null)
            {
                CustomerDB.DeleteCustomer(customer);
            }
        }
    }
}