using System.Net.WebSockets;
using System.Text;
using System.Collections.Concurrent;

public class WebSocketMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly ConcurrentDictionary<string, WebSocket> _connections = new();

    public WebSocketMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/ws/connect"))
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var proprietaireId = context.Request.Path.Value?.Split('/').Last();
                if (string.IsNullOrEmpty(proprietaireId))
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("ProprietaireId is required.");
                    return;
                }

                using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                _connections.TryAdd(proprietaireId, webSocket);
                await HandleWebSocketCommunication(webSocket, proprietaireId);
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("WebSocket connection is required.");
            }
        }
        else
        {
            await _next(context);
        }
    }

    private async Task HandleWebSocketCommunication(WebSocket webSocket, string proprietaireId)
    {
        var buffer = new byte[1024 * 4];
        try
        {
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }
            }
        }
        finally
        {
            _connections.TryRemove(proprietaireId, out _);
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
        }
    }

    public static async Task SendMessage(string proprietaireId, string message)
    {
        if (_connections.TryGetValue(proprietaireId, out var webSocket) && webSocket.State == WebSocketState.Open)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        }
        else
        {
            throw new Exception("WebSocket connection not found for this owner.");
        }
    }
}
