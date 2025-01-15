using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WebSocketController : ControllerBase
{
    [HttpPost("send/{proprietaireId}")]
    public async Task<IActionResult> SendMessage(string proprietaireId, [FromBody] string message)
    {
        try
        {
            await WebSocketMiddleware.SendMessage(proprietaireId, message);
            return Ok("Message sent.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

