
using MediatR;
using Microsoft.Extensions.Logging;
using Restuarants.Domain.Abstracts;
using Restuarants.Domain.Exceptions;
using Restuarants.Domain.Repositories;

namespace Restuarants.Application.Dishes.Commands.DeleteDishes
{
    public class DeleteDishesForRestuarantCommandHandler : IRequestHandler<DeleteDishesForRestuarantCommand>
    {
        private readonly ILogger<DeleteDishesForRestuarantCommand> _logger;
        private readonly IRestuarantsRepository _restuarantsRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IRestuarantAuthorizationService _restuarantAuthorizationService;

        public DeleteDishesForRestuarantCommandHandler(ILogger<DeleteDishesForRestuarantCommand> logger, IRestuarantsRepository restuarantsRepository,
            IDishRepository dishRepository, IRestuarantAuthorizationService restuarantAuthorizationService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _restuarantsRepository = restuarantsRepository ?? throw new ArgumentNullException(nameof(restuarantsRepository));
            _dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
            _restuarantAuthorizationService = restuarantAuthorizationService ?? throw new ArgumentNullException(nameof(restuarantAuthorizationService));
        }
        public async Task Handle(DeleteDishesForRestuarantCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting all dishes for the restuarant with Id: ", command.RestuarantId);

            var restuarant = await _restuarantsRepository.GetRestuarantAsync(command.RestuarantId);

            if (_restuarantAuthorizationService.Authorize(restuarant, Domain.Constants.ResourceOperation.Delete) == false)
            {
                throw new ForbidException();
            }

            if (restuarant == null)
            {
                throw new ApplicationException("Unable to delete dishes for the specified restuarant");
            }

            await _dishRepository.DeleteDishesAsync(restuarant.Dishes);
        }
    }
}
