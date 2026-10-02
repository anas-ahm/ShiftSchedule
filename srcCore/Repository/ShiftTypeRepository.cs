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
    
    // Read All ShiftTypes
    public List<ShiftType> ReadAllShiftTypes()
    {
        List<ShiftType> allShiftTypes = new List<ShiftType>();
        string sql = "SELECT * FROM ShiftType";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        using MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            ShiftType shiftTypeToAdd = new ShiftType();
            shiftTypeToAdd.ShiftTypeID = reader.GetInt32(reader.GetOrdinal("ShiftTypeID"));
            shiftTypeToAdd.Name = reader.GetString(reader.GetOrdinal("Name"));
            shiftTypeToAdd.StartTime = reader.GetTimeSpan(reader.GetOrdinal("StartTime"));
            shiftTypeToAdd.EndTime = reader.GetTimeSpan(reader.GetOrdinal("EndTime"));

            allShiftTypes.Add(shiftTypeToAdd);
        }

        return allShiftTypes;
    }
    
}