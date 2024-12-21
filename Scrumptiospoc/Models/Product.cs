namespace Scrumptiospoc.Models
{
    public class Product :BaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public Guid TenantId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSelected { get; set; } = false;

    }
}
