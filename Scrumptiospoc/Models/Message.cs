namespace Scrumptiospoc.Models
{
    public class Message :BaseModel
    {
        public Guid Id { get; set; }  
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
    }
}
