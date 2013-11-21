namespace TaxiOrNot.EntityModels
{
    public class Comment
    {
        public int Id { get; set; }

        public int Text { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int TaxiId { get; set; }

        public Taxi Taxi { get; set; }
    }
}