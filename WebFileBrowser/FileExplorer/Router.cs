namespace WebFileBrowser.FileExplorer;

public class Router
{
    public readonly RequestProcessor fallbackProcessor;

    private readonly BrowseRequestProcessor _browseRequestProcessor;

    public Router(RequestProcessor fallbackProcessor)
    {
        this.fallbackProcessor = fallbackProcessor;
        _browseRequestProcessor = new BrowseRequestProcessor();
    }

    public RequestProcessor Route(string requestPath)
    {
        requestPath = requestPath.ToLowerInvariant();
        switch (requestPath)
        {
            case "browse":
                return _browseRequestProcessor;
            
            default:
                return fallbackProcessor;
        }
    }
}