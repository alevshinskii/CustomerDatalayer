using System.Data.SqlClient;
using CustomerManagement.Entities;
using Respawn;

namespace CustomerManagement.Test.CustomerRepository;

public class CustomerRepositoryFixture
{
    private static Checkpoint checkpoint = new Checkpoint { };

    public void DeleteAll()
    {
        
        var repository = GetCustomerRepository();

        using var connection = repository.GetConnection();
        connection.Open();

        checkpoint.Reset(connection);

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