using CustomerRepository.Entities;
using CustomerRepository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace CustomerRepository.Repositories;

public class AddressRepository : BaseRepository, IRepository<Address>
{
    public Address Create(Address entity)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = new SqlCommand("INSERT INTO [Address] (AddressLine,AddressLine2,AddressType,City,Country,CustomerId,PostalCode,State) VALUES" +
                                     "(@AddressLine,@AddressLine2,@AddressType,@City,@Country,@CustomerId,@PostalCode,@State)", connection);

        var idParameter = new SqlParameter("@AddressId", SqlDbType.Int)
        {
            Value = entity.AddressId
        };
        var addressLineParameter = new SqlParameter("@AddressLine", SqlDbType.NVarChar, 100)
        {
            Value = entity.AddressLine
        };
        var addressLine2Parameter = new SqlParameter("@AddressLine2", SqlDbType.NVarChar, 100)
        {
            Value = entity.AddressLine2
        };
        var addressTypeParameter = new SqlParameter("@AddressType", SqlDbType.NVarChar, 20)
        {
            Value = entity.AddressType
        };
        var cityParameter = new SqlParameter("@City", SqlDbType.NVarChar, 50)
        {
            Value = entity.City
        };
        var countryParameter = new SqlParameter("@Country", SqlDbType.NVarChar, 100)
        {
            Value = entity.Country
        };
        var customerIdParameter = new SqlParameter("@CustomerId", SqlDbType.Int)
        {
            Value = entity.CustomerId
        };
        var postalCodeParameter = new SqlParameter("@PostalCode", SqlDbType.NVarChar, 6)
        {
            Value = entity.PostalCode
        };
        var stateParameter = new SqlParameter("@State", SqlDbType.NVarChar, 20)
        {
            Value = entity.State
        };

        command.Parameters.Add(idParameter);
        command.Parameters.Add(addressLineParameter);
        command.Parameters.Add(addressLine2Parameter);
        command.Parameters.Add(addressTypeParameter);
        command.Parameters.Add(cityParameter);
        command.Parameters.Add(countryParameter);
        command.Parameters.Add(customerIdParameter);
        command.Parameters.Add(postalCodeParameter);
        command.Parameters.Add(stateParameter);

        command.ExecuteNonQuery();

        var commandScope = new SqlCommand("SELECT IDENT_CURRENT('Address') as Scope", connection);
        using var reader = commandScope.ExecuteReader();
        if (reader.Read())
        {
            var idOfCreatedEntity = reader["Scope"];
            return Read(int.Parse(idOfCreatedEntity.ToString() ?? string.Empty));
        }
        return null;
    }

    public Address Read(int entityId)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = new SqlCommand("SELECT * FROM Address WHERE AddressId = @Id", connection);

        var idParam = new SqlParameter("@Id", SqlDbType.Int)
        {
            Value = entityId
        };

        command.Parameters.Add(idParam);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Address()
            {
                AddressId = reader.GetInt32("AddressId"),
                AddressLine = reader.GetString("AddressLine"),
                AddressLine2 = reader.GetString("AddressLine2"),
                AddressType = reader.GetString("AddressType"),
                CustomerId = reader.GetInt32("CustomerId"),
                City = reader.GetString("City"),
                Country = reader.GetString("Country"),
                PostalCode = reader.GetString("PostalCode"),
                State = reader.GetString("State"),
            };
        }
        return null;
    }

    public void Update(Address entity)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = new SqlCommand("UPDATE Address SET CustomerId=@CustomerId, AddressLine=@AddressLine, " +
                                     "AddressLine2=@AddressLine2, AddressType=@AddressType, City=@City, " +
                                     "Country=@Country, PostalCode=@PostalCode, State=@State" +
                                     " WHERE AddressId = @AddressId", connection);

        var idParameter = new SqlParameter("@AddressId", SqlDbType.Int)
        {
            Value = entity.AddressId
        };
        var addressLineParameter = new SqlParameter("@AddressLine", SqlDbType.NVarChar, 100)
        {
            Value = entity.AddressLine
        };
        var addressLine2Parameter = new SqlParameter("@AddressLine2", SqlDbType.NVarChar, 100)
        {
            Value = entity.AddressLine2
        };
        var addressTypeParameter = new SqlParameter("@AddressType", SqlDbType.NVarChar, 20)
        {
            Value = entity.AddressType
        };
        var cityParameter = new SqlParameter("@City", SqlDbType.NVarChar, 50)
        {
            Value = entity.City
        };
        var countryParameter = new SqlParameter("@Country", SqlDbType.NVarChar, 100)
        {
            Value = entity.Country
        };
        var customerIdParameter = new SqlParameter("@CustomerId", SqlDbType.Int)
        {
            Value = entity.CustomerId
        };
        var postalCodeParameter = new SqlParameter("@PostalCode", SqlDbType.NVarChar, 6)
        {
            Value = entity.PostalCode
        };
        var stateParameter = new SqlParameter("@State", SqlDbType.NVarChar, 20)
        {
            Value = entity.State
        };

        command.Parameters.Add(idParameter);
        command.Parameters.Add(addressLineParameter);
        command.Parameters.Add(addressLine2Parameter);
        command.Parameters.Add(addressTypeParameter);
        command.Parameters.Add(cityParameter);
        command.Parameters.Add(countryParameter);
        command.Parameters.Add(customerIdParameter);
        command.Parameters.Add(postalCodeParameter);
        command.Parameters.Add(stateParameter);

        command.ExecuteNonQuery();
    }

    public void Delete(int entityId)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = new SqlCommand("DELETE FROM Address WHERE AddressId = @Id", connection);

        var idParameter = new SqlParameter("@Id", SqlDbType.Int)
        {
            Value = entityId
        };

        command.Parameters.Add(idParameter);

        command.ExecuteNonQuery();

        connection.Close();
    }
}