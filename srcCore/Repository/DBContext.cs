namespace srcCore.Repository;
using DotNetEnv;

public class DBContext
{
    // DB Connection String
    public string ConnectToDB()
    {
        DirectoryInfo dir = new DirectoryInfo(AppContext.BaseDirectory);

        while (dir != null)
        {
            string path = Path.Combine(dir.FullName, "PedrosDBKeys.env");

            if (File.Exists(path))
            {
                Env.Load(path);
                break;
            }

            dir = dir.Parent;
        }

        string? connString = Environment.GetEnvironmentVariable("PedrosDB");

        if (string.IsNullOrEmpty(connString))
        {
            throw new InvalidOperationException("Connection string 'PedrosDB' not found. Is PedrosDBKeys.env in the project folder?");
        }

        return connString;
    }
}