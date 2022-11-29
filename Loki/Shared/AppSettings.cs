using Loki.Domain.Interfaces;

namespace Loki.Shared
{
    public class AppSettings : IAppSettings
    {
        public Database[] Databases { get; set; } = Array.Empty<Database>();
        public string? DefaultConnectionString { get; set; }
        public bool CollapseNavMenu { get; set; }
        //TODO: store database options..
    }
}
