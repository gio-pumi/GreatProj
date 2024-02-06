namespace GreatProj.Domain.DbEntities
{
    public class Client
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string RoomNumber { get; set; }
        public decimal Balance { get; set; }
        public long CountryId { get; set; }
        public Country Country { get; set; }
        public string Language { get; set; }
    }
}
