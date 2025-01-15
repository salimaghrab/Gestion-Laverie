namespace LaverieApi.Middleware
{
    public static class WebSocketExtension
    {
        public static IApplicationBuilder UseWebSocketMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebSocketMiddleware>();
        }
    }
}
