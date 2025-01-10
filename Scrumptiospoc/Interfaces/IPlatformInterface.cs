using Scrumptiospoc.Models;
using System.Collections.ObjectModel;

namespace Scrumptiospoc.Interfaces
{
    public interface IPlatformInterface
    {
        event Action StateChanged;
        public ObservableCollection<Platform> PlatformList { get; set; }
        public Task CreatePlatform();
    }
}
