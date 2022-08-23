using CustomerRepository.Entities;
using CustomerRepository.Test.CustomerRepository;
using System.Data.SqlClient;

namespace CustomerRepository.Test.AddressRepository;

public class AddressRepositoryFixture
{
    private readonly CustomerRepositoryFixture customerFixture = new CustomerRepositoryFixture();
    public async void DeleteAll()
    {
        var repository = GetAddressRepository();
        await using var connection = repository.GetConnection();
        connection.Open();

        var command = new SqlCommand("DELETE FROM Address", connection);

        command.ExecuteNonQuery();

        connection.Close();

        customerFixture.DeleteAll();
    }

    public Address GetAddress()
    {
        var customerRepository = customerFixture.GetCustomerRepository();
        var customer = customerRepository.Create(customerFixture.GetCustomer());

        return new Address()
        {
            AddressId = 1,
            AddressLine = "AddressLine",
            AddressLine2 = "AddressLine2",
            AddressType = "Shipping",
            City = "New York",
            Country = "United States",
            CustomerId = customer.Id,
            PostalCode = "123456",
            State = "New York"
        };
    }

    public Repositories.AddressRepository GetAddressRepository()
    {
        return new Repositories.AddressRepository();
    }
}