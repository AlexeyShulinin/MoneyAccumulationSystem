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
using MoneyAccumulationSystem.Services.Exceptions;

namespace MoneyAccumulationSystem.Services.Features.Incomes;

public class DeleteIncomeCommand : ICommand
{
    public int IncomeId { get; set; }
    
    public class DeleteIncomeCommandHandler : IRequestHandler<DeleteIncomeCommand>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public DeleteIncomeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }
        
        public async Task Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
        {
            var dbIncome = await unitOfWork.IncomeRepository.GetAsync(request.IncomeId, cancellationToken);
            if (dbIncome == null)
            {
                throw new NotFoundException();
            }

            if (dbIncome.UserId != httpContextAccessor.GetAuthUserFromClaims().Id)
            {
                throw new ForbiddenException();
            }

            unitOfWork.IncomeRepository.Delete(dbIncome);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
    
    public class DeleteIncomeCommandValidator : AbstractValidator<DeleteIncomeCommand>
    {
        public DeleteIncomeCommandValidator(MasDbContext dbContext)
        {
            RuleFor(x => x.IncomeId).GreaterThan(0);
        }
    }
}