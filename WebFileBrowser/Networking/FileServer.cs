using System.Net;
using WebFileBrowser.FileExplorer;

namespace WebFileBrowser.Networking;

public class FileServer
{
    private readonly HttpListener _http;
    private bool _shouldStop;
    
    public FileServer(params (string host, int port)[] addresses)
    {
        _http = new HttpListener();
        foreach (var (host, port) in addresses)
        {
            _http.Prefixes.Add($"http://{host}:{port}/");
        }
    }

    public void Start(Router router)
    {
        _http.Start();
        while (!_shouldStop)
        { 
            var ctx = _http.GetContext();
            HttpListenerRequest request = ctx.Request;
            HttpListenerResponse response = ctx.Response;

            RequestProcessor requestProcessor = router.Route(request.RawUrl ?? string.Empty);
            byte[] responseBytes = requestProcessor.BuildResponse(request);

            int contentLength = responseBytes.Length;
            response.ContentLength64 = contentLength;
            Stream output = response.OutputStream;
            output.Write(responseBytes,0,contentLength);
            output.Close();
        }
        
        _http.Close();
    }

    public void Stop()
    {
        _shouldStop = true;
    }
}