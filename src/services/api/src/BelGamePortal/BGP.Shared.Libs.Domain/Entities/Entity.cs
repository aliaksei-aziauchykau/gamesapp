using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BGP.Shared.Libs.Domain.Entities;
public abstract class Entity<T> : IHasKey<T>, IAuditable
{
    [JsonIgnore]
    [IgnoreDataMember]
    public DateTime Created { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public string? CreatedBy { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public DateTime? Updated { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public string? UpdatedBy { get; set; }

    public DomainEventCollection DomainEventCollection { get; } = new DomainEventCollection();
}
