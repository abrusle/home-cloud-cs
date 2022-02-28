using System.Net;
using System.Text;

namespace WebFileBrowser.FileExplorer;

public abstract class RequestProcessor
{
    public abstract byte[] BuildResponse(HttpListenerRequest request);
}

public sealed class RequestProcessorError : RequestProcessor
{
    /// <inheritdoc />
    public override byte[] BuildResponse(HttpListenerRequest request)
    {
        return Encoding.UTF8.GetBytes("<HTML><BODY>Error</BODY></HTML>");
    }
}

public abstract class RequestProcessorText : RequestProcessor
{
    private readonly StringBuilder _responseBuffer;

    /// <inheritdoc />
    protected RequestProcessorText()
    {
        _responseBuffer = new StringBuilder();
    }

    /// <inheritdoc />
    public override byte[] BuildResponse(HttpListenerRequest request)
    {
        Respond(request, _responseBuffer);
        
        byte[] buffer = Encoding.UTF8.GetBytes(_responseBuffer.ToString());
        
        _responseBuffer.Clear();
        return buffer;
    }

    protected abstract void Respond(HttpListenerRequest request, StringBuilder responseBuffer);
}