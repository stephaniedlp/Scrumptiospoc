using Scrumptiospoc.Interfaces;
using System.Collections.ObjectModel;
using Scrumptiospoc.Models;
using System.ComponentModel;

namespace Scrumptiospoc.Services
{
    public class LocationService : ILocationInterface
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public LocationService()
        {
            Locations = new();
        }

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

        public async Task OnOffLocation(Location location)
        {

            Location SelectedLocation = Locations.Where(x => x.Id == location.Id).SingleOrDefault();
            SelectedLocation.IsActive = !location.IsActive;
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
                Inventory = new Inventory() { Items = new List<InventoryItem>() } // Ensure Inventory is initialized

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
