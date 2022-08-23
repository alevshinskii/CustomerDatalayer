using System.Data.SqlClient;

namespace CustomerManagement.Repositories;

public class BaseRepository
{
    public SqlConnection GetConnection()
    {
        return new SqlConnection(GetConnectonString());
    }

    public string GetConnectonString()
    {
        return "Server=localhost\\sqlexpress;Database=CustomerLib_Levshinskii;Trusted_Connection=true;";
    }
}