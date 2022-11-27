using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loki.Domain.Interfaces
{
    public interface IAppSettingsService
    {
        IAppSettings AppSettings { get; }

        Task Save(IAppSettings appSettings);
        Task Load();

        event Action? OnChange;
    }
}
