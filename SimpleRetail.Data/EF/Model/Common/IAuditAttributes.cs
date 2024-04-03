namespace SimpleRetail.Data.EF.Model.Common;

public interface IAuditAttributes
{
    DateTime InsertDate { get; set; }
    Guid InsertUser { get; set; }
    DateTime? UpdateDate { get; set; }
    Guid? UpdateUser { get; set; }
}
