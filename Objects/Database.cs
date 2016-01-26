namespace ToDoList
{
  using System.Data;
  using System.Data.SqlClient;
  
  public class DB
  {
    public static SqlConnection Connection()
    {
      SqlConnection conn = new SqlConnection(DBConfiguration.connectionString);
      return conn;
    }
  }
}
