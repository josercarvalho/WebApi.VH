using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            
            IConfiguration config = builder.Build();
             
            string urlPath = config.GetValue<string>("ConnectionUrl:value"); 

            JsonSerializerOptions options = new JsonSerializerOptions();

            var url =  $@"{urlPath}{user}";

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