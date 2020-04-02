using FluentValidation;

using SocialVeterinary.Api.Validators;

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
            _mapper = mapper;
            _personRepository = personRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var persons = await _personRepository.GetAsync();
            return Ok(_mapper.Map<IEnumerable<PersonViewModel>>(persons));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(long id)
        {
            var person = await _personRepository.GetAsync(id);
            return Ok(_mapper.Map<PersonViewModel>(person));
        }


        [HttpPut]
        public async Task<ActionResult> Create([FromBody] CreatePersonViewModel request)
        {
            var domainPerson = _mapper.Map<Person>(request);

            var validator = new CreatePersonViewModelValidator();
            await validator.ValidateAndThrowAsync(request);

            var createdPerson = await _personRepository.AddAsync(domainPerson);
            return Ok(_mapper.Map<PersonViewModel>(createdPerson));
        }
    }
}
