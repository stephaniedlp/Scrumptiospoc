namespace Scrumptiospoc.Models
{
    public class Inventory
    {
        public Guid Id { get; set; }
        public Location Location { get; set; }
        public Guid Locationid { get; set; }    

        public List<InventoryItem> Items { get; set; }
    }
}
