using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGP.Shared.Libs.GameLogic.Interfaces;

public interface IWithConnectionId
{
    public string ConnectionId { get; set; }
}
