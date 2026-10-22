using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using ReceptekApi.Models;

namespace ReceptekApi.Controllers
{
    [Route("hozzavalo")]
    [ApiController]
    public class HozzavaloController : ControllerBase
    {
        MySqlConnection kapcsolat = new MySqlConnection("Server=localhost;Database=receptek;User=root;Password=;");
    }
}