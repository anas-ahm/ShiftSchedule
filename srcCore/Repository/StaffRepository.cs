namespace srcCore.Repository;
using Model;
using MySqlConnector;

public class StaffRepository
{
    
    
    // Create Staff
    public Staff CreateStaff(Staff staff)
    {
        string sql = "INSERT INTO Staff (Name, Phone, Email, IsLeader) VALUES (@Name, @Phone, @Email, @IsLeader)";
        
        MySqlConnection
        
    }
    
    // Read All Staff
    
    // Read Staff By ID
    
    // Update Staff
    
    // Delete Staff
}