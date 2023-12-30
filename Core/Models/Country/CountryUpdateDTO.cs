namespace GreatProj.Core.Models.Country
{
    public class CountryUpdateDTO
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public long? TranslationId { get; set; }
    }
}
