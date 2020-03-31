namespace SocialVeterinary.Domain
{
    using SocialVeterinary.Domain.Enums;

    public class Pet
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long OwnerId { get; set; }
        public PetType Type { get; set; }
    }
}
