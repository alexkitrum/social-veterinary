
namespace SocialVeterinary.Api.ViewModels
{
    using SocialVeterinary.Domain.Enums;

    public class PetViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public PetType Type { get; set; }
    }
}
