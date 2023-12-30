using GreatProj.Core.Models.Translation;

namespace GreatProj.Core.Models.Country
{
    public class CountryDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public TranslationDTO Translation { get; set; }
    }
}
