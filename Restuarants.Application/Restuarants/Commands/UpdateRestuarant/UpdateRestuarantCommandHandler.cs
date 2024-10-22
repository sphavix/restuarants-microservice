using MediatR;
using Microsoft.Extensions.Logging;
using Restuarants.Domain.Entities;
using Restuarants.Domain.Repositories;

namespace Restuarants.Application.Restuarants.Commands.UpdateRestuarant
{
    public class UpdateRestuarantCommandHandler : IRequestHandler<UpdateRestuarantCommand, bool>
    {
        private readonly IRestuarantsRepository _restuarantsRepository;
        private readonly ILogger<UpdateRestuarantCommandHandler> _logger;

        public UpdateRestuarantCommandHandler(IRestuarantsRepository restuarantsRepository, ILogger<UpdateRestuarantCommandHandler> logger)
        {
            _restuarantsRepository = restuarantsRepository ?? throw new ArgumentNullException(nameof(restuarantsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(UpdateRestuarantCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating restuarant with id: {RestuarantId} with {@UpdateRestuarant}", command.Id, command);

            var restuarant = await _restuarantsRepository.UpdateRestuarantAsync(new Restuarant
            {
                Id = command.Id,
                Name = command.Name,
                Description = command.Description,
                HasDelivery = command.HasDelivery
            });

            return restuarant;
        }
    }
}
