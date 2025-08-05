using Microsoft.AspNetCore.Mvc;

namespace ProvaSuficiencia.Controller;


[ApiController]
[Route("ping")]
public class PingController : ControllerBase
{

    [HttpGet]
    public IActionResult Ping()
    {
        return Ok("pong");
    }

}

