using System;
using System.Collections.Generic;
using System.Text;

using FluentAssertions;

using SocialVeterinary.Api.Validators;
using SocialVeterinary.Api.ViewModels;

using Xunit;

namespace SocialVeterinary.Api.Unittests.Validators
{
    public class CreatePersonViewModelValidatorTests
    {
        private const string TestFirstName = "John";
        private const string TestLastName = "Doe";

        [Fact]
        public void Validate_ViewModelIsValid_ShouldReturnValidResult()
        {
            var viewModel = new CreatePersonViewModel
                                {
                                    LastName = TestLastName,
                                    Name = TestFirstName
                                };
            var validator = CreateSut();
            var result = validator.Validate(viewModel);
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_EmptyViewModel_ShouldReturnErrorMessages()
        {
            var viewModel = new CreatePersonViewModel();
            var validator = CreateSut();
            var result = validator.Validate(viewModel);
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(2);
        }
        private CreatePersonViewModelValidator CreateSut()
        {
            return new CreatePersonViewModelValidator();
        }
    }
}
