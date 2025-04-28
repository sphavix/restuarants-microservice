using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restuarants.Domain.Abstracts;
using Restuarants.Domain.Entities;
using Restuarants.Domain.Exceptions;
using Restuarants.Domain.Repositories;

namespace Restuarants.Application.Restuarants.Commands.UpdateRestuarant
{
    public class UpdateRestuarantCommandHandler : IRequestHandler<UpdateRestuarantCommand>
    {
        private readonly IRestuarantsRepository _restuarantsRepository;
        private readonly ILogger<UpdateRestuarantCommandHandler> _logger;
        private readonly IRestuarantAuthorizationService _restuarantAuthorizationService;
        private readonly IMapper _mapper;

        public UpdateRestuarantCommandHandler(IRestuarantsRepository restuarantsRepository, ILogger<UpdateRestuarantCommandHandler> logger, 
            IRestuarantAuthorizationService restuarantAuthorizationService, IMapper mapper)
        {
            _restuarantsRepository = restuarantsRepository ?? throw new ArgumentNullException(nameof(restuarantsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _restuarantAuthorizationService = restuarantAuthorizationService ?? throw new ArgumentNullException(nameof(restuarantAuthorizationService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Handle(UpdateRestuarantCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating restuarant with id: {RestuarantId} with {@UpdateRestuarant}", command.Id, command);
            var restuarant = await _restuarantsRepository.GetRestuarantAsync(command.Id);

            if(restuarant is null)
            {
                throw new NotFoundException(nameof(Restuarant), command.Id.ToString());
            }

            if (!_restuarantAuthorizationService.Authorize(restuarant, Domain.Constants.ResourceOperation.Update))
            {
                throw new ForbidException();
            }
            //var restuarant = await _restuarantsRepository.UpdateRestuarantAsync(new Restuarant
            //{
            //    Id = command.Id,
            //    Name = command.Name,
            //    Description = command.Description,
            //    HasDelivery = command.HasDelivery
            //});
            _mapper.Map(command, restuarant);


            await _restuarantsRepository.SaveChanges();
        }
    }
}
