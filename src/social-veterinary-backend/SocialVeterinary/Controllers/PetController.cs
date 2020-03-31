
namespace SocialVeterinary.Api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using SocialVeterinary.Api.ViewModels;
    using SocialVeterinary.Domain;
    using SocialVeterinary.Domain.Interfaces;

    [ApiController]
    [Route("api/persons/{id:int}/pets")]
    public class PersonPetsController : ControllerBase
    {
        // Note: we are using repository class directly here just to simplify the code.
        // In real app, it should be Business Logic Layer between Presentation and Data.
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;

        public PersonPetsController(IMapper mapper, IPetRepository petRepository)
        {
            this._mapper = mapper;
            this._petRepository = petRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromRoute] long id)
        {
            var pets = await this._petRepository.GetPetsForOwnerAsync(id);
            return this.Ok(this._mapper.Map<IEnumerable<PetViewModel>>(pets));
        }

        [HttpPut]
        public async Task<ActionResult> Create([FromRoute] long id, [FromBody] CreatePetViewModel request)
        {
            var domainPet = this._mapper.Map<Pet>(request);
            domainPet.OwnerId = id;

            // TODO: Add validation here.

            var createdPet = await this._petRepository.AddAsync(domainPet);
            return this.Ok(this._mapper.Map<PetViewModel>(createdPet));
        }
    }
}
