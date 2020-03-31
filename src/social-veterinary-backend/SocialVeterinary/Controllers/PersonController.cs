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
    [Route("api/persons")]
    public class PersonController : ControllerBase
    {
        // Note: we are using repository class directly here just to simplify the code.
        // In real app, it should be Business Logic Layer between Presentation and Data.
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonController(IMapper mapper, IPersonRepository personRepository)
        {
            this._mapper = mapper;
            this._personRepository = personRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var persons = await this._personRepository.Get();
            return this.Ok(this._mapper.Map<IEnumerable<PersonViewModel>>(persons));
        }

        [HttpPut]
        public async Task<ActionResult> Create([FromBody] CreatePersonViewModel request)
        {
            var domainPerson = this._mapper.Map<Person>(request);

            // TODO: Add validation here.

            var createdPerson = await this._personRepository.AddAsync(domainPerson);
            return this.Ok(this._mapper.Map<PersonViewModel>(createdPerson));
        }
    }
}
