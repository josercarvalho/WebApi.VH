using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.VH.Interfaces;

namespace WebApi.VH.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;
        public readonly IUser _userService;

        public HomeController(ILogger<HomeController> logger, IUser userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("GetFibonacci/{id}")]
        public string Get(int id) => SequenciaFibonacci(id);

        private static string SequenciaFibonacci(int id)
        {
            int numeroAnterior = 0, numeroAtual = 1;

            string retorno = "0, 1 ";

            var sequenciaFibonacci = new List<int> { 0, 1 };

            while (sequenciaFibonacci.Count < id)
            {
                var novoNumero = sequenciaFibonacci[numeroAnterior] + sequenciaFibonacci[numeroAtual];
                sequenciaFibonacci.Add(novoNumero);
                retorno = retorno + ", " + novoNumero.ToString();

                numeroAtual++;
                numeroAnterior++;
            }

            return retorno;
        }

        [HttpGet("buscaPorId/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {

            if (String.IsNullOrEmpty(id)) return BadRequest("ID do usuário não pode ser 0 ou vazio!");

            var response = await _userService.BuscarUsuarioPorId(id);

            if (response.CodigoHttp == HttpStatusCode.OK)
            {
                return Ok(response.DadosRetorno);
            }
            else
            {
                return StatusCode((int)response.CodigoHttp, response.ErroRetorno);
            }

        }
    }
}