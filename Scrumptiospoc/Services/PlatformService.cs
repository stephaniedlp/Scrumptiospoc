
using Scrumptiospoc.Interfaces;
using Scrumptiospoc.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Scrumptiospoc.Services
{
    public class PlatformService : IPlatformInterface
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action? StateChanged;
        public PlatformService()
        {
            PlatformList = new();
        }
        private ObservableCollection<Platform> _platformslist;
        public ObservableCollection<Platform> PlatformList
        {
            get => _platformslist; set
            {
                if (_platformslist != value)
                {

                    _platformslist = value;
                    OnPropertyChanged(nameof(PlatformList));
                }
            }
        }
        public async Task CreatePlatform()
        {
            Platform platform = new Platform
            {
                Id = Guid.NewGuid(),
                Name = "Uber",
                UrlToken = "Url",
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            PlatformList.Add(platform);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
