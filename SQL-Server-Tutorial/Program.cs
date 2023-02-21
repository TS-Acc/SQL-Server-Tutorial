// no projects come with support for SQL Server. must be added seperately

// firstly we have to construct our connection string which will be below this comment

using Microsoft.Data.SqlClient;

string connectionString = "server=localhost\\sqlexpress;" +
                          "database=SalesDb;" +
                          "trusted_connection=true;" +
                          "trustServerCertificate=true;";

SqlConnection sqlConn = new SqlConnection(connectionString);

sqlConn.Open();

if(sqlConn.State != System.Data.ConnectionState.Open)
{
    throw new Exception("I screwed up my connection string!");
}

Console.WriteLine("Connection opened successfully!");

string sql = "SELECT * from Customers where sales > 90000 order by sales desc;";

SqlCommand cmd = new SqlCommand(sql, sqlConn);

SqlDataReader reader = cmd.ExecuteReader();

while (reader.Read())
{
    var id = Convert.ToInt32(reader["Id"]);
    var name = Convert.ToString(reader["Name"]);
    var city = Convert.ToString(reader["City"]);
    var state = Convert.ToString(reader["State"]);
    var sales = Convert.ToDecimal(reader["Sales"]);
    var active = Convert.ToBoolean(reader["Active"]);
    Console.WriteLine($"{id} | {name} | {city}, {state} | {sales} | {(active ? "Yes" : "No")}\n");
}

reader.Close();

Console.WriteLine("---------------------------------------------\n");

string sql2 = "SELECT * from Orders;";

SqlCommand cmd2 = new SqlCommand(sql2, sqlConn);

SqlDataReader reader2 = cmd2.ExecuteReader();

while (reader2.Read())
{
    var id = Convert.ToInt32(reader2["Id"]);
    var customerid = Convert.ToInt32(reader2["CustomerId"]);
    var date = Convert.ToDateTime(reader2["Date"]);
    var description = Convert.ToString(reader2["Description"]);
    Console.WriteLine($"{id} | {customerid} | {date} | {description}\n");
}

reader.Close();

sqlConn.Close();