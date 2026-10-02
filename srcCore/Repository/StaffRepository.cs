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

        MySqlCommand cmd = new MySqlCommand(ConnectDB());
        cmd.Parameters.AddWithValue("Name", staff.Name);
        cmd.Parameters.AddWithValue("Phone", staff.Phone);
        cmd.Parameters.AddWithValue("Email", staff.Email);
        cmd.Parameters.AddWithValue("IsLeader", staff.IsLeader);

        return staff;
    }
    
    // Read All Staff
    
    // Read Staff By ID
    
    // Update Staff
    
    // Delete Staff
}