using MediatR;
using Microsoft.Extensions.Logging;
using Restuarants.Domain.Repositories;

namespace Restuarants.Application.Restuarants.Commands.DeleteRestuarant
{
    public class DeleteRestuarantCommandHandler : IRequestHandler<DeleteRestuarantCommand, bool>
    {
        private readonly IRestuarantsRepository _restuarantsRepository;
        private readonly ILogger<DeleteRestuarantCommandHandler> _logger;

        public DeleteRestuarantCommandHandler(ILogger<DeleteRestuarantCommandHandler> logger, IRestuarantsRepository restuarantsRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _restuarantsRepository = restuarantsRepository ?? throw new ArgumentNullException(nameof(restuarantsRepository));
        }

        public async Task<bool> Handle(DeleteRestuarantCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting restuarant with an id {RestuarantId}", command.Id);

            var restuarant = await _restuarantsRepository.GetRestuarantAsync(command.Id);

            if(restuarant is null)
            {
                return false;
            }

            await _restuarantsRepository.DeleteRestuarantAsync(command.Id);

            return true;

        }
    }
}
