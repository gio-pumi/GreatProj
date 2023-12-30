namespace GreatProj.Domain.DbEntities
{
    public class Employee
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string Role { get; set; }
    }
}
