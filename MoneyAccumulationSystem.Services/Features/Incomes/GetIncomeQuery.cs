using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using MoneyAccumulationSystem.Repositories.UnitOfWork;
using MoneyAccumulationSystem.Services.DtoModels;
using MoneyAccumulationSystem.Services.Exceptions;
using MoneyAccumulationSystem.Services.Queries;

namespace MoneyAccumulationSystem.Services.Features.Incomes;

public class GetIncomeQuery : IQuery<IncomeDtoModel>
{
    public int Id { get; set; }
    public class GetIncomeQueryHandler : IRequestHandler<GetIncomeQuery, IncomeDtoModel>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GetIncomeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        
        public async Task<IncomeDtoModel> Handle(GetIncomeQuery request, CancellationToken cancellationToken)
        {
            var income = mapper.Map<IncomeDtoModel>(
                await unitOfWork.IncomeRepository.GetAsync(request.Id, cancellationToken));

            if (income == null)
            {
                throw new NotFoundException();
            }
            
            return income;
        }
    }
    
    public class GetIncomeQueryValidator : AbstractValidator<GetIncomeQuery>
    {
        public GetIncomeQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}