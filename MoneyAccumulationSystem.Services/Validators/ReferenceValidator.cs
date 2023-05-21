using System.Linq;
using FluentValidation;
using MoneyAccumulationSystem.CrossCutting.Interfaces;
using MoneyAccumulationSystem.Database.EF;
using MoneyAccumulationSystem.Services.DtoModels;

namespace MoneyAccumulationSystem.Services.Validators;

public class ReferenceValidator<TEntityModel> : AbstractValidator<ReferenceDtoModel>
    where TEntityModel : class, IBaseEntity
{
    public ReferenceValidator(MasDbContext dbContext)
    {
        RuleFor(x => x.Id)
            .Must(x => dbContext.Set<TEntityModel>().Any(e => e.Id == x))
            .WithMessage(x => $"{typeof(TEntityModel)} with Id {x.Id} wasn't found");
    }
}