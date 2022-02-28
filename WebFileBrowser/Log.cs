namespace WebFileBrowser;

public static class Log
{
    public static void Info(object obj) => Info(obj.ToString() ?? "null");
    
    public static void Info(string text)
    {
        Console.WriteLine("L : " + text);
    }

    public static void Warning(object obj) => Warning(obj.ToString() ?? "null");
    public static void Warning(string text)
    {
        Console.WriteLine("W : " + text);
    }

    public static void Error(object obj) => Error(obj.ToString() ?? "null");
    public static void Error(string text)
    {
        Console.WriteLine("E : " + text);
    }
}