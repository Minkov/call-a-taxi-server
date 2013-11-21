namespace TaxiOrNot.EntityModels
{
    public class TaxiImage
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int TaxiId { get; set; }

        public Taxi Taxi { get; set; }
    }
}