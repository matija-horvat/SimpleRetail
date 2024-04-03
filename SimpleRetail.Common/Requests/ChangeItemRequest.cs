﻿namespace SimpleRetail.Common.Requests;

public class ChangeItemRequest
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Manufacturer { get; set; }
    public bool Active { get; set; } = true;
    public Guid ChangeUserId { get; init; }
}
