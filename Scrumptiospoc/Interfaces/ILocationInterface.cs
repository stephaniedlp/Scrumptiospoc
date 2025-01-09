using Scrumptiospoc.Models;
using Scrumptiospoc.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Scrumptiospoc.Interfaces
{
    public interface ILocationInterface: INotifyPropertyChanged
    {
        event Action StateChanged;
        public ObservableCollection<Location> Locations { get; set; }
        public Task SelectLocation(Location location);
        public Task<Location> GetSelectedLocation();
        public Task ClearSelectedLocation();
        public Task CreateLocation();
        public Task ArchiveLocation(Location location);
        public void OnOffLocation(Location location);
        public void IsSlow(Location location);
    }
}
