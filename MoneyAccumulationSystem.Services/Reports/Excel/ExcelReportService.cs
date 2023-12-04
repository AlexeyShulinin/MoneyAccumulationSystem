using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MoneyAccumulationSystem.Services.Exceptions;
using OfficeOpenXml;

namespace MoneyAccumulationSystem.Services.Reports.Excel;

public class ExcelReportService : IReportService
{
    public async Task<byte[]> GenerateYearlyExcelReport(IDictionary<DateTime, decimal> incomes, string path, CancellationToken cancellationToken)
    {
        var dateFormat = "MMMM dd";
        const int incomeValueCol = 6;
        
        using var package = new ExcelPackage(path);

        if (!package.File.Exists)
        {
            throw new HandlerException("No such file or directory");
        }
        
        var worksheet = package.Workbook.Worksheets.First(x => "Yearly".Equals(x.Name));

        var cells = worksheet.Cells["D7:D30"];

        foreach (var income in incomes)
        {
            var date = income.Key.ToString(dateFormat);
            var cell = cells.FirstOrDefault(x => ((DateTime)x.Value).ToString(dateFormat) == date);

            if (cell != null)
            {
                worksheet.Cells[cell.Start.Row, incomeValueCol].Value = income.Value;
            }
        }

        using MemoryStream ms = new MemoryStream();
        await package.SaveAsAsync(ms, cancellationToken);
        ms.Seek(0, SeekOrigin.Begin);

        return ms.ToArray();
    }
}