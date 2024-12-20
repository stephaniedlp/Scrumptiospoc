namespace Scrumptiospoc.Models
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public List<Order>Orders{ get; set; }
    }
}
