using Scrumptiospoc.Interfaces;
using System.Collections.ObjectModel;
using Scrumptiospoc.Models;
using System.ComponentModel;

namespace Scrumptiospoc.Services
{
    public class LocationService : ILocationInterface
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public event Action? StateChanged;
        public LocationService()
        {
            Locations = new();
        }
        public Location SelectedLocation { get; set; }
        private ObservableCollection<Location> _locations;
        public ObservableCollection<Location> Locations
        {
            get => _locations;
            set
            {
                if (_locations != value)
                {

                    _locations = value;
                    OnPropertyChanged(nameof(Locations));
                }
            }
        }

        public async Task SelectLocation(Location loc)
        {
            SelectedLocation=loc;
        }

        public async Task<Location> GetSelectedLocation()
        {
            return SelectedLocation;
        }

        public async Task ClearSelectedLocation()
        {
            SelectedLocation = new();
        }

        
        private void NotifyStateChanged()
        {
            StateChanged?.Invoke();
        }

        public void OnOffLocation(Location location)
        {
            location.IsActive = !location.IsActive;
            if (location.IsActive)
            {
                location.LastOnline = DateTime.Now;
            }
            else
            {
                location.LastOffline = DateTime.Now;
            }
            location.Downtime =location.CalculateDownTime();
            NotifyStateChanged();
        }

        public void IsSlow(Location location) {
            location.IsSlow = !location.IsSlow;
        }

        public async Task CreateLocation()
        {
            Location location =
            new Location
            {
                Id = Guid.NewGuid(),
                Name = "Mock Name-" + Locations.Count().ToString(),
                Address = "123 Mock Street, Mock City",
                Latitude = 37.7749,    // San Francisco latitude for example
                Longitude = -122.4194, // San Francisco longitude for example
                IsActive = true,
                IsSlow = false,
                IsArchived = false,
                
            };

            Locations.Add(location);
            
        }
        public async Task ArchiveLocation(Location location)
        {
            Location SelectedLocation = Locations.Where(w => w.Id == location.Id).SingleOrDefault();
            SelectedLocation.IsArchived = !location.IsArchived;
        }
      
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
