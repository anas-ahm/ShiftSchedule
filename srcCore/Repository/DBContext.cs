namespace srcCore.Repository;
using DotNetEnv;

public class DBContext
{
    // DB Connection String
    public string ConnectToDB()
    {
        Env.Load("../PedrosDBKeys.env");

        string connString = Environment.GetEnvironmentVariable("PedrosDB");

        return connString;
    }
}