using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using WebApi.VH.Model;

namespace WebApi.VH.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

  
        [HttpGet(Name = "GetUrlUser")]
        public async Task<IActionResult> GetUrlUser(int user)
        {

            if (user == 0) return BadRequest("ID do usuário não pode ser 0 ou vazio!");
 
            string url; 
            JsonSerializerOptions options = new JsonSerializerOptions();

            url =  $@"https://gorest.co.in/public/v2/users/{user}";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var usuarios = JsonSerializer.Deserialize<Usuario>(content);
                    return Ok(usuarios);

                }
            }

            return StatusCode((int)Response.StatusCode);

        }
    }
}