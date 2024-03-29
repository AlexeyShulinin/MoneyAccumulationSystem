﻿using System;
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
using MoneyAccumulationSystem.Services.Validators;

namespace MoneyAccumulationSystem.Services.Features.Incomes;

public class CreateIncomeCommand : ICommand<int>
{
    public IncomeDtoModel Income { get; set; }
    
    public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommand, int>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CreateIncomeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<int> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
        {
            var authUser = httpContextAccessor.GetAuthUserFromClaims();
            var dbIncome = mapper.Map<Income>(request.Income);
            dbIncome.UserId = authUser.Id;

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