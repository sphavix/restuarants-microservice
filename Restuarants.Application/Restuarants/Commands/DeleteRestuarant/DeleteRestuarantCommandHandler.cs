using MediatR;
using Microsoft.Extensions.Logging;
using Restuarants.Domain.Abstracts;
using Restuarants.Domain.Exceptions;
using Restuarants.Domain.Repositories;

namespace Restuarants.Application.Restuarants.Commands.DeleteRestuarant
{
    public class DeleteRestuarantCommandHandler : IRequestHandler<DeleteRestuarantCommand, bool>
    {
        private readonly IRestuarantsRepository _restuarantsRepository;
        private readonly ILogger<DeleteRestuarantCommandHandler> _logger;
        private readonly IRestuarantAuthorizationService _restuarantAuthorizationService;

        public DeleteRestuarantCommandHandler(ILogger<DeleteRestuarantCommandHandler> logger, IRestuarantsRepository restuarantsRepository, IRestuarantAuthorizationService restuarantAuthorizationService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _restuarantsRepository = restuarantsRepository ?? throw new ArgumentNullException(nameof(restuarantsRepository));
            _restuarantAuthorizationService = restuarantAuthorizationService ?? throw new ArgumentNullException(nameof(restuarantAuthorizationService));
        }

        public async Task<bool> Handle(DeleteRestuarantCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting restuarant with an id {RestuarantId}", command.Id);

            var restuarant = await _restuarantsRepository.GetRestuarantAsync(command.Id);

            if(restuarant is null)
            {
                throw new NotFoundException($"Restuarant with {command.Id} does not exist, please try again");
            }

            if(_restuarantAuthorizationService.Authorize(restuarant, Domain.Constants.ResourceOperation.Delete) == false)
            {
                throw new ForbidException();
            }

            await _restuarantsRepository.DeleteRestuarantAsync(command.Id);

            return true;

        }
    }
}
