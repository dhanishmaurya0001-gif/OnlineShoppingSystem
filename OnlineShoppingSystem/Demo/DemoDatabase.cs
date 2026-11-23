namespace OnlineShoppingSystem.Demo;

public static class DemoDatabase
{
    public static string ConnectionString(IConfiguration config)
    {
        var path = config["DEMO_DB_PATH"] ?? Path.Combine(Path.GetTempPath(), "dhanish-shopping-demo.db");
        var directory = Path.GetDirectoryName(Path.GetFullPath(path));
        if (directory is not null) Directory.CreateDirectory(directory);
        return $"Data Source={path}";
    }
}
