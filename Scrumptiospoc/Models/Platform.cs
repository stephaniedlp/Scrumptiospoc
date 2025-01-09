namespace Scrumptiospoc.Models
{
    public class Platform : BaseModel
    {
        public Platform()
        {

        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;        
        public string IntegrationToken { get; set; }
        public string Logo { get; set; }
        public string ExternalId { get; set; }


    }
}
