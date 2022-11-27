using Newtonsoft.Json;
using Loki.Domain.Interfaces;

namespace Loki.Shared
{
    public class AppSettingsService : IAppSettingsService
    {
        private const string Key = "settings";
        private bool _isInitialDataLoadComplete;

        private IAppSettings? _settings;
        public IAppSettings AppSettings 
        { 
            get 
            { 
                return _settings ?? new AppSettings(); 
            } 
            set
            {
                _settings = value;
                NotifyDataChanged();
            }
        }

        public async Task Save(IAppSettings appSettings)
        {
            await SecureStorage.Default.SetAsync(Key, JsonConvert.SerializeObject(appSettings));
            AppSettings = appSettings;
        }

        public async Task Load()
        {
            if (_isInitialDataLoadComplete) return;
            _isInitialDataLoadComplete = true;
            var json = await SecureStorage.Default.GetAsync(Key);
            if (json is null) return;
            AppSettings = JsonConvert.DeserializeObject<AppSettings>(json)!;
        }

        public event Action? OnChange;
        private void NotifyDataChanged() => OnChange?.Invoke();
    }
}
