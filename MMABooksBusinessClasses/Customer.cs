using System;

namespace MMABooksBusinessClasses
{
    public class Customer
    {
        public Customer() { }

        public Customer(int id, string name, string address, string city, string state, string zipcode)
        {
            CustomerID = id;
            Name = name;
            Address = address;
            City = city;
            State = state;
            ZipCode = zipcode;
        }

        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public override string ToString()
        {
            return $"{Name}, {Address}, {City}, {State} {ZipCode}";
        }

        public string Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return "Name is required.";
            if (string.IsNullOrWhiteSpace(Address))
                return "Address is required.";
            if (string.IsNullOrWhiteSpace(City))
                return "City is required.";
            if (string.IsNullOrWhiteSpace(State) || State.Length != 2)
                return "State must be a 2 character code.";
            if (string.IsNullOrWhiteSpace(ZipCode))
                return "ZipCode is required.";
            return null;
        }
    }
}
