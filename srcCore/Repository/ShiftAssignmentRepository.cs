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
    
    // Read All ShiftAssignments
    public List<ShiftAssignment> ReadAllShiftAssignments()
    {
        List<ShiftAssignment> allAssignments = new List<ShiftAssignment>();
        string sql = "SELECT * FROM ShiftAssignments";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        using MySqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            ShiftAssignment assignmentToAdd = new ShiftAssignment();
            assignmentToAdd.ShiftAssignmentID = reader.GetInt32(reader.GetOrdinal("AssignmentID"));
            assignmentToAdd.StaffID = reader.GetInt32(reader.GetOrdinal("StaffID"));
            assignmentToAdd.ShiftID = reader.GetInt32(reader.GetOrdinal("ShiftID"));

            allAssignments.Add(assignmentToAdd);
        }

        return allAssignments;
    }
    
    // Read ShiftAssignment By ID
    public ShiftAssignment ReadShiftAssignmentByID(int id)
    {
        string sql = "SELECT * FROM ShiftAssignments WHERE AssignmentID = @id";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("id", id);
        using MySqlDataReader reader = cmd.ExecuteReader();

        ShiftAssignment assignmentToRead = new ShiftAssignment();

        while (reader.Read())
        {
            assignmentToRead.ShiftAssignmentID = reader.GetInt32(reader.GetOrdinal("AssignmentID"));
            assignmentToRead.StaffID = reader.GetInt32(reader.GetOrdinal("StaffID"));
            assignmentToRead.ShiftID = reader.GetInt32(reader.GetOrdinal("ShiftID"));
        }

        return assignmentToRead;
    }
    
    // Update ShiftAssignment
    public ShiftAssignment UpdateShiftAssignment(ShiftAssignment assignment)
    {
        string sql = "UPDATE ShiftAssignments SET StaffID = @StaffID, ShiftID = @ShiftID WHERE AssignmentID = @AssignmentID";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("AssignmentID", assignment.ShiftAssignmentID);
        cmd.Parameters.AddWithValue("StaffID", assignment.StaffID);
        cmd.Parameters.AddWithValue("ShiftID", assignment.ShiftID);

        cmd.ExecuteNonQuery();

        return assignment;
    }
    
    // Delete ShiftAssignment
    public void DeleteShiftAssignment(ShiftAssignment assignment)
    {
        string sql = "DELETE FROM ShiftAssignments WHERE AssignmentID = @AssignmentID";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("AssignmentID", assignment.ShiftAssignmentID);
        cmd.ExecuteNonQuery();
    }