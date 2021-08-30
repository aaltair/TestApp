using System.Threading.Tasks;
using AutoMapper;
using TestApp.Core.Dtos.Idenitiy;
using TestApp.Persistent.Interfaces;
using TestApp.Services.Idenitiy.Interfaces;
using TestApp.Services.RequestService.Interfaces;

namespace TestApp.Services.Idenitiy
{
    public class UserServices : IUserServices
    {
        private readonly ITestAppUnitOfWork _unitOfWork;
        private readonly ICurrentRequestService _currentRequest;
        private readonly IMapper _mapper;

        public UserServices(ITestAppUnitOfWork unitOfWork,
                            ICurrentRequestService currentRequest,
                            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _currentRequest = currentRequest;
            _mapper = mapper;
        }
        public Task<LoginSuccessInfoDto> Login(LoginDto loginDto)
        {
            throw new System.NotImplementedException();
        }
    }
}