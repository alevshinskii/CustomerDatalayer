using System.Data.SqlClient;
using CustomerRepository.Entities;
using Respawn;
using Respawn.Graph;

namespace CustomerRepository.Test.CustomerRepository;

public class CustomerRepositoryFixture
{

    public async void DeleteAll()
    {
        var repository = GetCustomerRepository();
        await using var connection = repository.GetConnection();
        connection.Open();

        var command = new SqlCommand("DELETE FROM Customer", connection);

        command.ExecuteNonQuery();

        connection.Close();

    }

    public Customer GetCustomer()
    {
        return new Customer()
        {
            Id = 0,
            LastName = "LastName",
            FirstName = "FirstName",
            PhoneNumber = "42738947298347",
            Email = "email@email.com",
            TotalPurchasesAmount = 1000
        };
    }

    public Repositories.CustomerRepository GetCustomerRepository()
    {
        return new Repositories.CustomerRepository();
    }
}