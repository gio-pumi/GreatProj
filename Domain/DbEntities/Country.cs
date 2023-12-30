namespace GreatProj.Domain.DbEntities
{
    public class Country
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long TranslationId { get; set; }
        public Translation Translation { get; set; }
    }
}
