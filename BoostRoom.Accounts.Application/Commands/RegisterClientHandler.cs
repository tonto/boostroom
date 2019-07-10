using System;
using System.Threading;
using System.Threading.Tasks;
using BoostRoom.Accounts.Domain;
using BoostRoom.Accounts.Domain.ClientAggregate;
using MediatR;

namespace BoostRoom.Accounts.Application.Commands
{
    public class RegisterClientHandler : IRequestHandler<RegisterClient>
    {
        //private readonly RegistrationService _registrationService;

        //public RegisterClientHandler(RegistrationService registrationService)
        //{
        //    _registrationService = registrationService;
        //}

        public Task<Unit> Handle(RegisterClient request, CancellationToken cancellationToken)
        {
            // TODO - Validate command 
            throw new NotImplementedException();
            
            //return await Unit.Task;
            
            //await _registrationService.RegisterClient(
            //    request.Username,
            //    request.Email,
            //    request.Password,
            //    request.FirstName,
            //    request.LastName,
            //    request.AddressLine,
            //    request.City,
            //    request.Zip,
            //    request.Country,
            //    request.SubscribeToOffers);

            //return await Unit.Task;
        }
    }
}