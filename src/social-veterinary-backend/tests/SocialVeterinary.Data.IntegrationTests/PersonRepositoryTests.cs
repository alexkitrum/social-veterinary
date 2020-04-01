using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.Extensions.Configuration;

using SocialVeterinary.Data.IntegrationTests.DbHelpers;
using SocialVeterinary.Domain;

using Xunit;

namespace SocialVeterinary.Data.IntegrationTests
{
    public class PersonRepositoryTests
    {
        private readonly PersonDbHelper _personDbHelper;

        private readonly IConfiguration Configuration;

        public PersonRepositoryTests()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _personDbHelper = new PersonDbHelper();
        }

        [Fact]
        public async Task Get_SeveralExistingPersons_ShouldReturnAllPersons()
        {
            Person createdPerson = null;
            try
            {
                createdPerson = await _personDbHelper.AddAsync(new Person
                                                                   {
                                                                       Name = "tes1t",
                                                                       LastName = "hey"
                                                                   });

                var repository = CreateSut();
                var persons = await repository.GetAsync();
                persons.Should().BeEquivalentTo(createdPerson);
            }
            finally
            {

                if (createdPerson != null)
                {
                    await _personDbHelper.DeleteAsync(new[] { createdPerson.Id });
                }
                
            }
        }

        private PersonRepository CreateSut()
        {
            return new PersonRepository(Configuration);
        }
    }
}
