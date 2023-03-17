using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MoneyAccumulationSystem.Repositories.UnitOfWork;
using MoneyAccumulationSystem.Services.DtoModels;
using MoneyAccumulationSystem.Services.Queries;

namespace MoneyAccumulationSystem.Services.Features.Users;

public class GetUserListQuery : IQuery<IList<UserDtoModel>>
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, IList<UserDtoModel>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GetUserListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        
        public async Task<IList<UserDtoModel>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<List<UserDtoModel>>(await unitOfWork.UsersRepository.GetListAsync(cancellationToken));
        }
    }
}