using System.Threading.Tasks;
using TestApp.Core.Dtos.Idenitiy;

namespace TestApp.Services.Idenitiy.Interfaces
{
    public interface IUserServices
    {
        Task<LoginSuccessInfoDto> Login(LoginDto loginDto);
    }
}