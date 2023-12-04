using System;
using MoneyAccumulationSystem.CrossCutting.Interfaces;

namespace MoneyAccumulationSystem.Database.EF.Models;

public class BaseEntity : IBaseEntity
{
    public int Id { get; set; }

    public DateTimeOffset? CreatedDate { get; set; }
    public int? CreatedByUserId { get; set; }

    public DateTimeOffset? UpdatedDate { get; set; }
    public int? UpdatedByUserId { get; set; }

    public User CreatedByUser { get; set; }
    public User UpdatedByUser { get; set; }
}