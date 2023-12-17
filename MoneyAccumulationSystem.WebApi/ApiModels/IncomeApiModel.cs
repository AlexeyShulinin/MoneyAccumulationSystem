using System;

namespace MoneyAccumulationSystem.WebApi.ApiModels;

public class IncomeApiModel : BaseApiModel
{
    public decimal Amount { get; set; }
    public DateTimeOffset? DateTimeOffset { get; set; }
    public string Notes { get; set; }

    public CurrencyReferenceApiModel Currency { get; set; }
    public ReferenceApiModel IncomeType { get; set; }
}