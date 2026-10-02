namespace srcCore.Repository;
using Model;
using MySqlConnector;

public class ShiftTypeRepository
{
    // DB Connection String
    private string ConnectDB()
    { 
        DBContext _dbContext = new DBContext();

        string connection = _dbContext.ConnectToDB();

        return connection;
    }
    
    // Create ShiftType
    public ShiftType CreateShiftType(ShiftType shiftType)
    {
        string sql = "INSERT INTO ShiftType (Name, StartTime, EndTime) VALUES (@Name, @StartTime, @EndTime)";
        
        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();
        
        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("Name", shiftType.Name);
        cmd.Parameters.AddWithValue("StartTime", shiftType.StartTime);
        cmd.Parameters.AddWithValue("EndTime", shiftType.EndTime);

        cmd.ExecuteNonQuery();

        return shiftType;
    }
    
}