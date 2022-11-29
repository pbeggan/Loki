using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loki.Domain.Interfaces
{
    public interface IAppSettings
    {
        Database[] Databases { get; set; }
        string? DefaultConnectionString { get; set; }
        bool CollapseNavMenu { get; set; }
        // time zone
    }

    public class Database
    {
        public Database(string name, string connectionString)
        {
            Name = name.Trim();
            ConnectionString = connectionString.Trim();
        }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public bool IsValid => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(ConnectionString);
    }
}
