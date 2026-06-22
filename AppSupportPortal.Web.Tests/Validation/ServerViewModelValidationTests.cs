using AppSupportPortal.Web.Models;
using FluentAssertions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace AppSupportPortal.Web.Tests.Validation
{
    public class ServerViewModelValidationTests
    {
        private List<ValidationResult> Validate(ServerViewModel model)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, new ValidationContext(model), results, true);
            return results;
        }

        [Fact]
        public void Model_IsInvalid_WhenNameMissing()
        {
            var model = new ServerViewModel
            {
                IPAddress = "10.0.0.1"
            };

            var results = Validate(model);

            results.Should().Contain(r => r.MemberNames.Contains("Name"));
        }

        [Fact]
        public void Model_IsInvalid_WhenIpAddressMissing()
        {
            var model = new ServerViewModel
            {
                Name = "Server A"
            };

            var results = Validate(model);

            results.Should().Contain(r => r.MemberNames.Contains("IPAddress"));
        }

        [Fact]
        public void Model_IsValid_WhenAllFieldsPresent()
        {
            var model = new ServerViewModel
            {
                Name = "Server A",
                IPAddress = "10.0.0.1"
            };

            var results = Validate(model);

            results.Should().BeEmpty();
        }
    }
}
