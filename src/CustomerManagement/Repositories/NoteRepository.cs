using System.Data;
using System.Data.SqlClient;
using CustomerManagement.Entities;
using CustomerManagement.Interfaces;

namespace CustomerManagement.Repositories;

public class NoteRepository:BaseRepository,IRepository<Note>
{
    public Note Create(Note entity)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = new SqlCommand("INSERT INTO [Notes] (CustomerId,Note) " +
                                     "VALUES (@CustomerId, @Note)", connection);

        var customerIdParameter = new SqlParameter("@CustomerId", SqlDbType.Int)
        {
            Value = entity.CustomerId
        };
        var textParameter = new SqlParameter("@Note", SqlDbType.NVarChar, 255)
        {
            Value = entity.Text
        };

        command.Parameters.Add(customerIdParameter);
        command.Parameters.Add(textParameter);

        command.ExecuteNonQuery();

        var commandScope = new SqlCommand("SELECT IDENT_CURRENT('Notes') as Id", connection);
        using var reader = commandScope.ExecuteReader();
        int idOfCreatedEntity = 0;
        if (reader.Read())
        {
            idOfCreatedEntity = int.Parse(reader["Id"].ToString() ?? string.Empty);
        }
        return Read(idOfCreatedEntity);
    }

    public Note Read(int entityId)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = new SqlCommand("SELECT * FROM Notes WHERE NoteId = @Id", connection);

        var idParam = new SqlParameter("@Id", SqlDbType.Int)
        {
            Value = entityId
        };

        command.Parameters.Add(idParam);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return new Note()
            {
                Id=reader.GetInt32("NoteId"),
                CustomerId = reader.GetInt32("CustomerId"),
                Text = reader.GetString("Note")
            };
        }
        return null;
    }

    public void Update(Note entity)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = new SqlCommand("UPDATE Notes SET CustomerId=@CustomerId, Note=@Note " +
                                     " WHERE NoteId = @NoteId", connection);


        var noteIdParameter = new SqlParameter("@NoteId", SqlDbType.Int)
        {
            Value = entity.Id
        };
        var customerIdParameter = new SqlParameter("@CustomerId", SqlDbType.Int)
        {
            Value = entity.CustomerId
        };
        var textParameter = new SqlParameter("@Note", SqlDbType.NVarChar, 255)
        {
            Value = entity.Text
        };

        command.Parameters.Add(noteIdParameter);
        command.Parameters.Add(customerIdParameter);
        command.Parameters.Add(textParameter);

        command.ExecuteNonQuery();
    }

    public void Delete(int entityId)
    {
        using var connection = GetConnection();
        connection.Open();
        var command = new SqlCommand("DELETE FROM Notes WHERE NoteId = @Id", connection);

        var idParameter = new SqlParameter("@Id", SqlDbType.Int)
        {
            Value = entityId
        };

        command.Parameters.Add(idParameter);

        command.ExecuteNonQuery();

        connection.Close();
    }
}