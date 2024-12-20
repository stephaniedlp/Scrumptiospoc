using Scrumptiospoc.Models;
using Scrumptiospoc.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Scrumptiospoc.Interfaces
{
    public interface ILocationInterface: INotifyPropertyChanged
    {
        public ObservableCollection<Location> Locations { get; set; }

        public Task CreateLocation();
        public Task ArchiveLocation(Location location);
        public Task OnOffLocation(Location location);
    }
}
