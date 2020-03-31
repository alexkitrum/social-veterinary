
namespace SocialVeterinary.Api
{
    using AutoMapper;

    using SocialVeterinary.Api.ViewModels;
    using SocialVeterinary.Domain;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            this.CreateMap<Pet, PetViewModel>();
            this.CreateMap<CreatePetViewModel, Pet>();

            this.CreateMap<Person, PersonViewModel>();
            this.CreateMap<CreatePersonViewModel, Person>();

        }
    }
}
