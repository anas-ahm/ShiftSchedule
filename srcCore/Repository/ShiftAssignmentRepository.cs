namespace srcCore.Repository;
using Model;
using MySqlConnector;

public class ShiftAssignmentRepository : IShiftAssignmentRepository
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
        string sql = "INSERT INTO ShiftAssignment (StaffID, ShiftID) VALUES (@StaffID, @ShiftID)";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("StaffID", assignment.StaffID);
        cmd.Parameters.AddWithValue("ShiftID", assignment.ShiftID);

        cmd.ExecuteNonQuery();
        
        // Finds and takes the id from DB
        assignment.ShiftAssignmentID = (int)cmd.LastInsertedId;

        return assignment;
    }

    // Update ShiftAssignment
    public ShiftAssignment UpdateShiftAssignment(ShiftAssignment assignment)
    {
        string sql =
            "UPDATE ShiftAssignment SET StaffID = @StaffID, ShiftID = @ShiftID WHERE AssignmentID = @AssignmentID";

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
        string sql = "DELETE FROM ShiftAssignment WHERE AssignmentID = @AssignmentID";

        using MySqlConnection connString = new MySqlConnection(ConnectDB());
        connString.Open();

        MySqlCommand cmd = new MySqlCommand(sql, connString);
        cmd.Parameters.AddWithValue("AssignmentID", assignment.ShiftAssignmentID);
        cmd.ExecuteNonQuery();
    }
}
