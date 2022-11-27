using Loki.Domain.Interfaces;

namespace Loki.Shared
{
    public class AppSettings : IAppSettings
    {
        public string? DefaultConnectionString { get; set; }
        public bool CollapseNavMenu { get; set; }
        //TODO: store database options..
    }
}
