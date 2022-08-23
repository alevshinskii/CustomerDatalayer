using System.Data;
using System.Data.SqlClient;
using CustomerManagement.Entities;
using CustomerManagement.Interfaces;

namespace CustomerManagement.Repositories;

public class CustomerRepository : BaseRepository, IRepository<Customer>
{
    public Customer Create(Customer entity)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = new SqlCommand("INSERT INTO [Customer] (FirstName,LastName,PhoneNumber,Email,TotalPurchasesAmount) VALUES" +
                                     "(@FirstName, @LastName, @PhoneNumber, @Email, @TotalPurchasesAmount)", connection);

        var firstNameParameter = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50)
        {
            Value = entity.FirstName
        };
        var lastNameParameter = new SqlParameter("@LastName", SqlDbType.NVarChar, 50)
        {
            Value = entity.LastName
        };
        var phoneNumberParameter = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 15)
        {
            Value = entity.PhoneNumber
        };
        var emailParameter = new SqlParameter("@Email", SqlDbType.NVarChar, 255)
        {
            Value = entity.Email
        };
        var totalPurchasesAmountParameter = new SqlParameter("@TotalPurchasesAmount", SqlDbType.Money)
        {
            Value = entity.TotalPurchasesAmount
        };

        command.Parameters.Add(firstNameParameter);
        command.Parameters.Add(lastNameParameter);
        command.Parameters.Add(phoneNumberParameter);
        command.Parameters.Add(emailParameter);
        command.Parameters.Add(totalPurchasesAmountParameter);

        command.ExecuteNonQuery();

        var commandScope = new SqlCommand("SELECT IDENT_CURRENT('Customer') as Id", connection);
        using var reader = commandScope.ExecuteReader();
        int idOfCreatedEntity = 0;
        if (reader.Read())
        {
            idOfCreatedEntity = int.Parse(reader["Id"].ToString() ?? string.Empty);
        }
        return Read(idOfCreatedEntity);
    }

    public Customer Read(int entityId)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = new SqlCommand("SELECT * FROM Customer WHERE CustomerId = @Id", connection);

        var idParam = new SqlParameter("@Id", SqlDbType.Int)
        {
            Value = entityId
        };

        command.Parameters.Add(idParam);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Customer()
            {
                Id = reader.GetInt32("CustomerId"),
                FirstName = reader.GetString("FirstName"),
                LastName = reader.GetString("LastName"),
                Email = reader["Email"].ToString(),
                PhoneNumber = reader["PhoneNumber"].ToString(),
                TotalPurchasesAmount = decimal.Parse(reader["TotalPurchasesAmount"].ToString())
            };
        }
        return null;
    }

    public void Update(Customer entity)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = new SqlCommand("UPDATE Customer SET FirstName=@FirstName, LastName=@LastName, Email=@Email, PhoneNumber=@PhoneNumber, TotalPurchasesAmount=@TotalPurchasesAmount" +
                                     " WHERE CustomerId = @Id", connection);

        var idParameter = new SqlParameter("@Id", SqlDbType.Int)
        {
            Value = entity.Id
        };
        var firstNameParameter = new SqlParameter("@FirstName", SqlDbType.NVarChar, 50)
        {
            Value = entity.FirstName
        };
        var lastNameParameter = new SqlParameter("@LastName", SqlDbType.NVarChar, 50)
        {
            Value = entity.LastName
        };
        var phoneNumberParameter = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 15)
        {
            Value = entity.PhoneNumber
        };
        var emailParameter = new SqlParameter("@Email", SqlDbType.NVarChar, 255)
        {
            Value = entity.Email
        };
        var totalPurchasesAmountParameter = new SqlParameter("@TotalPurchasesAmount", SqlDbType.Money)
        {
            Value = entity.TotalPurchasesAmount
        };

        command.Parameters.Add(idParameter);
        command.Parameters.Add(firstNameParameter);
        command.Parameters.Add(lastNameParameter);
        command.Parameters.Add(phoneNumberParameter);
        command.Parameters.Add(emailParameter);
        command.Parameters.Add(totalPurchasesAmountParameter);


        command.ExecuteNonQuery();
    }

    public void Delete(int entityId)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = new SqlCommand("DELETE FROM Customer WHERE CustomerId = @Id", connection);

        var idParameter = new SqlParameter("@Id", SqlDbType.Int)
        {
            Value = entityId
        };

        command.Parameters.Add(idParameter);

        command.ExecuteNonQuery();

        connection.Close();
    }

}
