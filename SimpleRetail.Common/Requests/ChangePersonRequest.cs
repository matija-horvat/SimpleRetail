namespace SimpleRetail.Common.Requests;

public class ChangePersonRequest
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public bool Active { get; set; } = true;
    public Guid ChangeUserId { get; init; }
}

