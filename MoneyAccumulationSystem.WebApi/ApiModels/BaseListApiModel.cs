using System.Collections.Generic;

namespace MoneyAccumulationSystem.WebApi.ApiModels;

public class BaseListApiModel<T> where T : BaseApiModel
{
    public IList<T> Items { get; set; }

    public BaseListApiModel(IList<T> items)
    {
        Items = items;
    }
}