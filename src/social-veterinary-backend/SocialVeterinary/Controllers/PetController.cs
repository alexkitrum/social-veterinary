
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
            _mapper = mapper;
            _petRepository = petRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromRoute] long id)
        {
            var pets = await _petRepository.GetPetsForOwnerAsync(id);
            return Ok(_mapper.Map<IEnumerable<PetViewModel>>(pets));
        }

        [HttpPut]
        public async Task<ActionResult> Create([FromRoute] long id, [FromBody] CreatePetViewModel request)
        {
            var domainPet = _mapper.Map<Pet>(request);
            domainPet.OwnerId = id;

            // TODO: Add validation here.

            var createdPet = await _petRepository.AddAsync(domainPet);
            return Ok(_mapper.Map<PetViewModel>(createdPet));
        }
    }
}
