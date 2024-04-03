using System.ComponentModel.DataAnnotations;

namespace SimpleRetail.Common.Requests;

public class ChangeSupplierRequest
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string LocationHQ { get; set; } = string.Empty;
    public Guid ContactId { get; set; }
    public bool Active { get; set; } = true;
    public Guid ChangeUserId { get; init; }
}
