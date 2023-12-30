namespace GreatProj.Domain.DbEntities
{
    public class User
    {
        public long Id { get; set; }
        public string Mail { get; set; }
        public string Number { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
