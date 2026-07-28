using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGP.Shared.Libs.Domain.Entities;
public interface IHasDomainEventCollection
{
    DomainEventCollection DomainEventCollection { get; }
}
