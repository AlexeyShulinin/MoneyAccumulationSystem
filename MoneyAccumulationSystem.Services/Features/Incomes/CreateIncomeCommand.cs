using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using MoneyAccumulationSystem.Database.EF;
using MoneyAccumulationSystem.Database.EF.Models;
using MoneyAccumulationSystem.Repositories.UnitOfWork;
using MoneyAccumulationSystem.Services.Commands;
using MoneyAccumulationSystem.Services.DtoModels;
using MoneyAccumulationSystem.Services.Validators;

namespace MoneyAccumulationSystem.Services.Features.Incomes;

public class CreateIncomeCommand : ICommand<int>
{
    public IncomeDtoModel Income { get; set; }
    
    public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommand, int>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CreateIncomeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        
        public async Task<int> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
        {
            var dbIncome = mapper.Map<Income>(request.Income);

            unitOfWork.IncomeRepository.Create(dbIncome);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return dbIncome.Id;
        }
    }
    
    public class CreateIncomeCommandValidator : AbstractValidator<CreateIncomeCommand>
    {
        public CreateIncomeCommandValidator(MasDbContext dbContext)
        {
            RuleFor(x => x.Income.Id)
                .Equal(0);
            RuleFor(x => x.Income.DateTimeOffset)
                .GreaterThanOrEqualTo(DateTimeOffset.MinValue);
            RuleFor(x => x.Income.Notes)
                .MaximumLength(1000);
            RuleFor(x => x.Income.IncomeType)
                .SetValidator(new ReferenceValidator<IncomeType>(dbContext))
                .When(x => x.Income.IncomeType != null);
            RuleFor(x => x.Income.User)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .SetValidator(new ReferenceValidator<User>(dbContext));
        }
    }
}