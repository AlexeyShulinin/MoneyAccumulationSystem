using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using MoneyAccumulationSystem.Repositories.UnitOfWork;
using MoneyAccumulationSystem.Services.Queries;
using MoneyAccumulationSystem.Services.Reports;

namespace MoneyAccumulationSystem.Services.Features.Reports;

public class GenerateYearlyReportQuery : IQuery<byte[]>
{
    public int Year { get; set; }

    public class GenerateYearlyReportQueryHandler : IRequestHandler<GenerateYearlyReportQuery, byte[]>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IReportService reportService;
        public GenerateYearlyReportQueryHandler(IUnitOfWork unitOfWork, IReportService reportService)
        {
            this.unitOfWork = unitOfWork;
            this.reportService = reportService;
        }
        
        public async Task<byte[]> Handle(GenerateYearlyReportQuery request, CancellationToken cancellationToken)
        {
            var path = "..\\MoneyAccumulationSystem.Services\\Reports\\Excel\\Templates\\Solbeg_Year_Report.xlsx";

            var yearlyIncomesDict = await unitOfWork.IncomeRepository
                .GetYearlyDictionaryAsync(request.Year, cancellationToken);
            
            return await reportService.GenerateYearlyExcelReport(yearlyIncomesDict, path, cancellationToken);
        }
    }
    
    public class GenerateYearlyReportQueryValidator : AbstractValidator<GenerateYearlyReportQuery>
    {
        public GenerateYearlyReportQueryValidator()
        {
            RuleFor(x => x.Year).GreaterThan(0).LessThan(3000);
        }
    }
}