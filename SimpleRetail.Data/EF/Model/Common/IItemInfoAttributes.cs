using System.ComponentModel.DataAnnotations;

namespace SimpleRetail.Data.EF.Model.Common;

public interface IItemInfoAttributes
{
    Guid ItemId { get; set; }
    decimal ItemQuantity { get; set; }
    decimal ItemUnitPrice { get; set; }
}
