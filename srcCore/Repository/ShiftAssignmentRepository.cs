namespace srcCore.Repository;
using Model;
using MySqlConnector;
public class ShiftAssignmentRepository
{
    // DB Connection String
    private string ConnectDB()
    { 
        DBContext _dbContext = new DBContext();

        string connection = _dbContext.ConnectToDB();

        return connection;
    }
    
    // Create ShiftAssignment
    public ShiftAssignment CreateShiftAssignment(ShiftAssignment assignment)
    {
        string sql = "INSERT INTO ShiftAssignments (StaffID, ShiftID) VALUES (@StaffID, @ShiftID)";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();
        
        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("StaffID", assignment.StaffID);
        cmd.Parameters.AddWithValue("ShiftID", assignment.ShiftID);

        cmd.ExecuteNonQuery();

        return assignment;
    }
    
}