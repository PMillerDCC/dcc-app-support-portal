using AppSupportPortal.Web.Models;
using FluentAssertions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace AppSupportPortal.Web.Tests.Validation
{
    public class UserViewModelValidationTests
    {
        private List<ValidationResult> Validate(UserViewModel model)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, new ValidationContext(model), results, true);
            return results;
        }

        [Fact]
        public void Model_IsInvalid_WhenUsernameMissing()
        {
            var model = new UserViewModel
            {
                Email = "test@example.com"
            };

            var results = Validate(model);

            results.Should().Contain(r => r.MemberNames.Contains("UserName"));
        }

        [Fact]
        public void Model_IsInvalid_WhenEmailMissing()
        {
            var model = new UserViewModel
            {
                UserName = "phil"
            };

            var results = Validate(model);

            results.Should().Contain(r => r.MemberNames.Contains("Email"));
        }

        [Fact]
        public void Model_IsValid_WhenAllFieldsPresent()
        {
            var model = new UserViewModel
            {
                UserName = "phil",
                Email = "test@example.com"
            };

            var results = Validate(model);

            results.Should().BeEmpty();
        }
    }
}