using System.Threading;
using System.Threading.Tasks;
using BoostRoom.Accounts.Domain;
using MediatR;

namespace BoostRoom.Accounts.Application.Commands
{
    public class RegisterSellerHandler : IRequestHandler<RegisterSeller>
    {
        private readonly RegistrationService _registrationService;

        public RegisterSellerHandler(RegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        public async Task<Unit> Handle(RegisterSeller request, CancellationToken cancellationToken)
        {
            await _registrationService.RegisterSeller(
                request.Username, 
                request.Email, 
                request.Password,
                request.Country);

            return await Unit.Task;
        }
    }
}