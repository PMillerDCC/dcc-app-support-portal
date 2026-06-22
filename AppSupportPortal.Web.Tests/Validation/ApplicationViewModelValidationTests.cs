using AppSupportPortal.Web.Models;
using FluentAssertions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace AppSupportPortal.Web.Tests.Validation
{
    public class ApplicationViewModelValidationTests
    {
        private List<ValidationResult> Validate(ApplicationViewModel model)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, new ValidationContext(model), results, true);
            return results;
        }

        [Fact]
        public void Model_IsInvalid_WhenDescriptionMissing()
        {
            var model = new ApplicationViewModel
            {
                Name = "Test",
                ServerId = 1
            };

            var results = Validate(model);

            results.Should().Contain(r => r.MemberNames.Contains("Description"));
        }

        [Fact]
        public void Model_IsInvalid_WhenServerIdMissing()
        {
            var model = new ApplicationViewModel
            {
                Name = "Test",
                Description = "Desc"
            };

            var results = Validate(model);

            results.Should().Contain(r => r.MemberNames.Contains("ServerId"));
        }

        [Fact]
        public void Model_IsValid_WhenAllFieldsPresent()
        {
            var model = new ApplicationViewModel
            {
                Name = "Test",
                Description = "Desc",
                ServerId = 1
            };

            var results = Validate(model);

            results.Should().BeEmpty();
        }
    }
}