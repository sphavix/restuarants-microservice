using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restuarants.Application.Restuarants.Dtos;
using Restuarants.Application.Users.Abstract;
using Restuarants.Domain.Entities;
using Restuarants.Domain.Repositories;

namespace Restuarants.Application.Restuarants.Commands.CreateRestuarant
{
    public class CreateRestuarantCommandHandler : IRequestHandler<CreateRestuarantCommand, int>
    {
        private readonly IRestuarantsRepository _restuarantsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        IUserContext _userContext;

        public CreateRestuarantCommandHandler(ILogger<CreateRestuarantCommandHandler> logger, IRestuarantsRepository restuarantsRepository, IMapper mapper, IUserContext userContext)
        {
            _restuarantsRepository = restuarantsRepository ?? throw new ArgumentNullException(nameof(restuarantsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        public async Task<int> Handle(CreateRestuarantCommand command, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            _logger.LogInformation("{UserEmail} [{UserId}] is Creating a new restuarant {Restuarant}", currentUser.Email, currentUser.Id, command);

            // Map entity to Dto
            var restuarant = _mapper.Map<Restuarant>(command);
            restuarant.OwnerId = currentUser.Id;

            int id = await _restuarantsRepository.CreateRestuarantAsync(restuarant);

            return id;
        }
    }
}
