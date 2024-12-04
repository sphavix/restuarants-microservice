
using MediatR;
using Microsoft.Extensions.Logging;
using Restuarants.Domain.Repositories;

namespace Restuarants.Application.Dishes.Commands.DeleteDishes
{
    public class DeleteDishesForRestuarantCommandHandler : IRequestHandler<DeleteDishesForRestuarantCommand>
    {
        private readonly ILogger<DeleteDishesForRestuarantCommand> _logger;
        private readonly IRestuarantsRepository _restuarantsRepository;
        private readonly IDishRepository _dishRepository;

        public DeleteDishesForRestuarantCommandHandler(ILogger<DeleteDishesForRestuarantCommand> logger, IRestuarantsRepository restuarantsRepository,
            IDishRepository dishRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _restuarantsRepository = restuarantsRepository ?? throw new ArgumentNullException(nameof(restuarantsRepository));
            _dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
        }
        public async Task Handle(DeleteDishesForRestuarantCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting all dishes for the restuarant with Id: ", command.RestuarantId);

            var restuarant = await _restuarantsRepository.GetRestuarantAsync(command.RestuarantId);

            if(restuarant == null)
            {
                throw new ApplicationException("Unable to delete dishes for the specified restuarant");
            }

            await _dishRepository.DeleteDishesAsync(restuarant.Dishes);
        }
    }
}
