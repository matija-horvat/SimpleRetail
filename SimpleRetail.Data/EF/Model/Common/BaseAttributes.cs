using System.ComponentModel.DataAnnotations;

namespace SimpleRetail.Data.EF.Model.Common;

public class BaseAttributes
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public string Code { get; set; } = string.Empty;
    [Required]
    public string Name { get; set; } = string.Empty;
}
