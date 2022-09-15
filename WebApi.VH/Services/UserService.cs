using System.Dynamic;
using System.Text.Json;
using WebApi.VH.Interfaces;
using WebApi.VH.Model;

namespace WebApi.VH.Services
{
    public class UserService : IUser
    {
        public async  Task<ResponseGenerico<Usuario>> BuscarUsuarioPorId(string id)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();
            string urlPath = config.GetValue<string>("ConnectionUrl:value");

            var url = $"{urlPath}{id}";
            var response = new ResponseGenerico<Usuario>();

            using (var httpClient = new HttpClient())
            {
                var responseApi = await httpClient.GetAsync(url);
                var contentResp = await responseApi.Content.ReadAsStringAsync();
                var objResponse = JsonSerializer.Deserialize<Usuario>(contentResp);

                if (responseApi.IsSuccessStatusCode)
                {
                    response.CodigoHttp = responseApi.StatusCode;
                    response.DadosRetorno = objResponse;
                }
                else
                {
                    response.CodigoHttp = responseApi.StatusCode;
                    response.ErroRetorno = JsonSerializer.Deserialize<ExpandoObject>(contentResp);
                }
            }

            return response;
        }
    }
}
