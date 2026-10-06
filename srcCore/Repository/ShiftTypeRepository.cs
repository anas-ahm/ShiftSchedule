namespace srcCore.Repository;
using Model;
using MySqlConnector;

public class ShiftTypeRepository : IShiftTypeRepository
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
    
    // Read ShiftType By ID
    public ShiftType ReadShiftTypeByID(int id)
    {
        string sql = "SELECT * FROM ShiftType WHERE ShiftTypeID = @id";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("id", id);
        using MySqlDataReader reader = cmd.ExecuteReader();

        ShiftType shiftTypeToRead = new ShiftType();

        while (reader.Read())
        {
            shiftTypeToRead.ShiftTypeID = reader.GetInt32(reader.GetOrdinal("ShiftTypeID"));
            shiftTypeToRead.Name = reader.GetString(reader.GetOrdinal("Name"));
            shiftTypeToRead.StartTime = reader.GetTimeSpan(reader.GetOrdinal("StartTime"));
            shiftTypeToRead.EndTime = reader.GetTimeSpan(reader.GetOrdinal("EndTime"));
        }

        return shiftTypeToRead;
    }
    
    // Update ShiftType
    public ShiftType UpdateShiftType(ShiftType shiftType)
    {
        string sql = "UPDATE ShiftType SET Name = @Name, StartTime = @StartTime, EndTime = @EndTime WHERE ShiftTypeID = @ShiftTypeID";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("ShiftTypeID", shiftType.ShiftTypeID);
        cmd.Parameters.AddWithValue("Name", shiftType.Name);
        cmd.Parameters.AddWithValue("StartTime", shiftType.StartTime);
        cmd.Parameters.AddWithValue("EndTime", shiftType.EndTime);

        cmd.ExecuteNonQuery();

        return shiftType;
    }
    
    // Delete ShiftType
    public void DeleteShiftType(ShiftType shiftType)
    {
        string sql = "DELETE FROM ShiftType WHERE ShiftTypeID = @ShiftTypeID";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("ShiftTypeID", shiftType.ShiftTypeID);
        cmd.ExecuteNonQuery();
    }
}