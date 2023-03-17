using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MoneyAccumulationSystem.Repositories.UnitOfWork;
using MoneyAccumulationSystem.Services.DtoModels;
using MoneyAccumulationSystem.Services.Queries;

namespace MoneyAccumulationSystem.Services.Features.Incomes;

public class GetIncomeListQuery : IQuery<IList<IncomeDtoModel>>
{
    public class GetIncomeListQueryHandler : IRequestHandler<GetIncomeListQuery, IList<IncomeDtoModel>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GetIncomeListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        
        public async Task<IList<IncomeDtoModel>> Handle(GetIncomeListQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<List<IncomeDtoModel>>(await unitOfWork.IncomeRepository.GetListAsync(cancellationToken));
        }
    }
}