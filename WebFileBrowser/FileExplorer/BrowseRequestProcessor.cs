using System.Net;
using System.Text;

namespace WebFileBrowser.FileExplorer;

public class BrowseRequestProcessor : RequestProcessorText
{
    /// <inheritdoc />
    protected override void Respond(HttpListenerRequest request, StringBuilder responseBuffer)
    {
        responseBuffer.Append($"<HTML><BODY>Hello world!<br>Requested path: {request.Url}</BODY></HTML>");
    }
}