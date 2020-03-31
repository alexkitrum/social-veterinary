namespace SocialVeterinary.Api.ViewModels
{
    using SocialVeterinary.Domain.Enums;

    public class CreatePetViewModel
    {
        public string Name { get; set; }

        public PetType Type { get; set; }
    }
}
