namespace TaxiOrNot.EntityModels
{
    public class Vote
    {
        public int Id { get; set; }


        public int VoteTypeId { get; set; }
        public virtual VoteType VoteType { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int TaxiId { get; set; }

        public virtual Taxi Taxi { get; set; }
    }
}