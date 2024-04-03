namespace SimpleRetail.Common.Responses;

public class StoreDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public bool? Active { get; set; }
    public PersonDto Contact { get; set; }
}
