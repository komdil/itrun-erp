namespace Domain.Entities
{
    public class Building
    {
        public Guid Id { get; set; }
        public string BuildingId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Area { get; set; }
    }
}
