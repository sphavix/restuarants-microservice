

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restuarants.Domain.Abstracts;
using Restuarants.Domain.Entities;
using Restuarants.Domain.Exceptions;
using Restuarants.Domain.Repositories;

namespace Restuarants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, Guid>
    {
        private readonly ILogger<CreateDishCommandHandler> _logger;
        private readonly IRestuarantsRepository _restuarantRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;
        private readonly IRestuarantAuthorizationService _restuarantAuthorizationService;

        public CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger, IRestuarantsRepository restuarantRepository, 
            IDishRepository dishRepository, IMapper mapper, IRestuarantAuthorizationService restuarantAuthorizationService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _restuarantRepository = restuarantRepository ?? throw new ArgumentNullException(nameof(restuarantRepository));
            _dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _restuarantAuthorizationService = restuarantAuthorizationService ?? throw new ArgumentNullException(nameof(restuarantAuthorizationService));

        }
        public async Task<Guid> Handle(CreateDishCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new dish for the restuarant", command);

            var restuarant = await _restuarantRepository.GetRestuarantAsync(command.RestuarantId);

            if(restuarant is null)
            {
                throw new ApplicationException("Error occured while trying to create a new dish!");
            }

            if (_restuarantAuthorizationService.Authorize(restuarant, Domain.Constants.ResourceOperation.Create) == false)
            {
                throw new ForbidException();
            }

            var dish = _mapper.Map<Dish>(command);

            return await _dishRepository.CreateDishAsync(dish);

        }
    }
}
