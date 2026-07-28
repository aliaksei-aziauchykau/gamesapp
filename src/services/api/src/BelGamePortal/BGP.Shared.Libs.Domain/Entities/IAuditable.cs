namespace BGP.Shared.Libs.Domain.Entities;

public interface IAuditable
{
    DateTime Created { get; set; }

    string? CreatedBy { get; set; }

    DateTime? Updated { get; set; }

    string? UpdatedBy { get; set; }
}
