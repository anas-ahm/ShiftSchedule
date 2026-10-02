namespace srcCore.Repository;
using Model;
using MySqlConnector;

public class StaffRepository
{
    
    // DB Connection String
    private string ConnectDB()
    { 
        DBContext _dbContext = new DBContext();

        string connection = _dbContext.ConnectToDB();

        return connection;
    }
    
    
    // Create Staff
    public Staff CreateStaff(Staff staff)
    {
        
        string sql = "INSERT INTO Staff (Name, Phone, Email, IsLeader) VALUES (@Name, @Phone, @Email, @IsLeader)";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();
        
        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("Name", staff.Name);
        cmd.Parameters.AddWithValue("Phone", staff.Phone);
        cmd.Parameters.AddWithValue("Email", staff.Email);
        cmd.Parameters.AddWithValue("IsLeader", staff.IsLeader);

        cmd.ExecuteNonQuery();

        return staff;
    }
    
    // Read All Staff
    public List<Staff> ReadAllStaff()
    {
        
        
        List<Staff> allStaff = new List<Staff>();
        string sql = "SELECT * FROM Staff";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        using MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Staff staffToAdd = new Staff();
            staffToAdd.StaffID = reader.GetInt32(reader.GetOrdinal("StaffID"));
            staffToAdd.Name = reader.GetString(reader.GetOrdinal("Name"));
            staffToAdd.Phone = reader.GetString(reader.GetOrdinal("Phone"));
            staffToAdd.Email = reader.GetString(reader.GetOrdinal("Email"));
            staffToAdd.IsLeader = reader.GetBoolean(reader.GetOrdinal("IsLeader"));
            
            allStaff.Add(staffToAdd);
        }
        
        return allStaff;
    }
    
    // Read Staff By ID
    
    // Update Staff
    
    // Delete Staff
}