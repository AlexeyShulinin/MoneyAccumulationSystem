using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyAccumulationSystem.Services.Reports;

public interface IReportService
{
    Task<byte[]> GenerateYearlyExcelReport(IDictionary<DateTime, decimal> incomes, string path, CancellationToken cancellationToken);
}