using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using MoneyAccumulationSystem.CrossCutting.Extensions;
using MoneyAccumulationSystem.Database.EF;
using MoneyAccumulationSystem.Database.EF.Models;
using MoneyAccumulationSystem.Repositories.UnitOfWork;
using MoneyAccumulationSystem.Services.Commands;
using MoneyAccumulationSystem.Services.DtoModels;
using MoneyAccumulationSystem.Services.Exceptions;
using MoneyAccumulationSystem.Services.Validators;

namespace MoneyAccumulationSystem.Services.Features.Incomes;

public class UpdateIncomeCommand : ICommand<IncomeDtoModel>
{
    public int IncomeId { get; set; }
    public IncomeDtoModel Income { get; set; }
    
    public class UpdateIncomeCommandHandler : IRequestHandler<UpdateIncomeCommand, IncomeDtoModel>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UpdateIncomeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        
        public async Task<IncomeDtoModel> Handle(UpdateIncomeCommand request, CancellationToken cancellationToken)
        {
            var dbIncome = await unitOfWork.IncomeRepository.GetAsync(request.IncomeId, cancellationToken);
            if (dbIncome == null)
            {
                throw new NotFoundException();
            }
            
            mapper.Map(request.Income, dbIncome);
            unitOfWork.IncomeRepository.Update(dbIncome);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<IncomeDtoModel>(dbIncome);
        }
    }
    
    public class UpdateIncomeCommandValidator : AbstractValidator<UpdateIncomeCommand>
    {
        public UpdateIncomeCommandValidator(MasDbContext dbContext)
        {
            RuleFor(x => x.IncomeId)
                .Equal(x => x.Income.Id);
            RuleFor(x => x.Income.Id)
                .GreaterThan(0);
            RuleFor(x => x.Income.DateTimeOffset)
                .GreaterThanOrEqualTo(DateTimeOffset.MinValue)
                .When(x => x.Income.DateTimeOffset != null);
            RuleFor(x => x.Income.Notes)
                .MaximumLength(1000);
            RuleFor(x => x.Income.IncomeType)
                .SetValidator(new ReferenceValidator<IncomeType>(dbContext))
                .When(x => x.Income.IncomeType != null);
        }
    }
}