using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SeDevOps.Api.Controllers;
using SeDevOps.Api.Dtos;
using SeDevOps.Data;
using Xunit;

namespace SeDevOps.Api.Tests
{
    public class SecurityTests
    {
        private readonly IMapper _mapper;

        public SecurityTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(ApplicationsController).Assembly);
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Update_ShouldRejectInvalidModelState()
        {
            // Arrange
            var db = TestDbContextFactory.Create();
            var controller = new ApplicationsController(db, _mapper);

            // Force model state to be invalid (simulating malicious or malformed input)
            controller.ModelState.AddModelError("Name", "Required");

            var dto = new ApplicationUpdateDto
            {
                Id = 1,
                Name = null, // invalid
                Description = "Test",
                ServerId = 1
            };

            // Act
            var result = await controller.Update(1, dto);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}