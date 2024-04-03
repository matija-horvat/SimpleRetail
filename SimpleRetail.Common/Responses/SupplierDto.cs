namespace SimpleRetail.Common.Responses;

public class SupplierDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string LocationHQ { get; set; } = string.Empty;
    public bool Active { get; set; }
    public PersonDto Contact { get; set; }
}
