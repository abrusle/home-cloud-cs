using WebFileBrowser.FileExplorer;
using WebFileBrowser.Networking;

namespace WebFileBrowser;

public static class Program
{
    public static Config Config => _config;
    private static Config _config;

    private static FileServer? _fileServer;
    
    public static void Main(string[] args)
    {
        Log.Info("Start of Main.");

        _config.serveDir = Environment.GetEnvironmentVariable("SERVE_DIR") ?? string.Empty;
        LoadEnvFile();

        Start();
    }

    /// <summary>
    /// Beginning of the program after initialization is complete.
    /// </summary>
    private static void Start()
    {
        _fileServer = new FileServer(
            ("localhost", 80),
            ("127.0.0.1", 80));
        _fileServer.Start(new Router(new RequestProcessorError()));
    }

    private static void LoadEnvFile()
    {
        string envPath = Path.Combine(Environment.CurrentDirectory, ".env");
        if (!File.Exists(envPath))
        {
            Log.Warning("No .env found in current directory.");
            return;
        }

        var lines = File.ReadAllLines(envPath);
        foreach (string line in lines)
        {
            var args = line.Split(' ');
            if (args.Length < 2) continue;
            SetConfigForKey(args[0], args[1]);
        }
    }
    
    private static void SetConfigForKey(string key, string value)
    {
        key = key.ToUpperInvariant();
        switch (key)
        {
            case "SERVE_DIR":
                _config.serveDir = value;
                break;
            default: return;
        }
        
        Log.Info("Set Config : " + key + " = " + value);
    }
}