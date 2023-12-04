using System;

namespace MoneyAccumulationSystem.Services.DtoModels;

public class IncomeDtoModel : BaseDtoModel
{
    public decimal Amount { get; set; }
    public DateTimeOffset? DateTimeOffset { get; set; }
    public string Notes { get; set; }

    public ReferenceDtoModel User { get; set; }
    public ReferenceDtoModel IncomeType { get; set; }
}