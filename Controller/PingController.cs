using Microsoft.AspNetCore.Mvc;

namespace ProvaSuficiencia.Controller;


[ApiController]
[Route("ping")]
public class PingController : ControllerBase
{

    public IActionResult Ping()
    {
        return Ok("pong");
    }

}

