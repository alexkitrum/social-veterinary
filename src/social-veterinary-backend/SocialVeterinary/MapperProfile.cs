
namespace SocialVeterinary.Api
{
    using AutoMapper;

    using SocialVeterinary.Api.ViewModels;
    using SocialVeterinary.Domain;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Pet, PetViewModel>();
            CreateMap<CreatePetViewModel, Pet>();

            CreateMap<Person, PersonViewModel>();
            CreateMap<CreatePersonViewModel, Person>();

        }
    }
}
