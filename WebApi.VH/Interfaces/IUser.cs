using WebApi.VH.Model;
using WebApi.VH.Services;

namespace WebApi.VH.Interfaces
{
    public interface IUser
    {
        Task<ResponseGenerico<Usuario>> BuscarUsuarioPorId(string id);
    }
}
