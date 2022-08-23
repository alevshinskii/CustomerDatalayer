using System.Data.SqlClient;
using CustomerRepository.Entities;
using CustomerRepository.Test.CustomerRepository;

namespace CustomerRepository.Test.NoteRepository;

public class NoteRepositoryFixture
{
    private readonly CustomerRepositoryFixture customerFixture = new CustomerRepositoryFixture();
    public async void DeleteAll()
    {
        var repository = GetNoteRepository();
        await using var connection = repository.GetConnection();
        connection.Open();

        var command = new SqlCommand("DELETE FROM Notes", connection);

        command.ExecuteNonQuery();

        connection.Close();

        customerFixture.DeleteAll();
    }

    public Note GetNote()
    {
        var customerRepository = customerFixture.GetCustomerRepository();
        var customer = customerRepository.Create(customerFixture.GetCustomer());

        return new Note()
        {
            Id = 1,
            CustomerId = customer.Id,
            Text = "Some text"
        };
    }

    public Repositories.NoteRepository GetNoteRepository()
    {
        return new Repositories.NoteRepository();
    }
}