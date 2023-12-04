using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using MoneyAccumulationSystem.Repositories.UnitOfWork;
using MoneyAccumulationSystem.Services.DtoModels;
using MoneyAccumulationSystem.Services.Exceptions;
using MoneyAccumulationSystem.Services.Queries;

namespace MoneyAccumulationSystem.Services.Features.Users;

public class GetUserQuery : IQuery<UserDtoModel>
{
    public int? Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDtoModel>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GetUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        
        public async Task<UserDtoModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<UserDtoModel>(
                await unitOfWork.UsersRepository
                    .GetAsync(request.Id, request.Login, request.Password, cancellationToken));

            if (user == null)
            {
                throw new NotFoundException();
            }
            
            return user;
        }
    }
    
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .When(x => x.Id != null);
            
            RuleFor(x => x.Login).NotEmpty().When(x => x.Id == null);
            RuleFor(x => x.Password).NotEmpty().When(x => x.Id == null);
        }
    }
}