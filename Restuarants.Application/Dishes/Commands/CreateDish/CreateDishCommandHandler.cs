

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restuarants.Domain.Entities;
using Restuarants.Domain.Repositories;

namespace Restuarants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, Guid>
    {
        private readonly ILogger<CreateDishCommandHandler> _logger;
        private readonly IRestuarantsRepository _restuarantRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger, IRestuarantsRepository restuarantRepository, 
            IDishRepository dishRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _restuarantRepository = restuarantRepository ?? throw new ArgumentNullException(nameof(restuarantRepository));
            _dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<Guid> Handle(CreateDishCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new dish for the restuarant", command);

            var restuarant = await _restuarantRepository.GetRestuarantAsync(command.RestuarantId);

            if(restuarant is null)
            {
                throw new ApplicationException("Error occured while trying to create a new dish!");
            }

            var dish = _mapper.Map<Dish>(command);

            return await _dishRepository.CreateDishAsync(dish);

        }
    }
}
