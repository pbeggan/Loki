using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loki.Domain.Interfaces
{
    public interface IAppSettings
    {
        string? DefaultConnectionString { get; set; }
        bool CollapseNavMenu { get; set; }
        // time zone
    }
}
