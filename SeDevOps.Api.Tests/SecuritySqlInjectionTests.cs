using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SeDevOps.Api.Controllers;
using SeDevOps.Api.Dtos;
using SeDevOps.Data;
using SeDevOps.Data.Entities;
using Xunit;

namespace SeDevOps.Api.Tests
{
    public class SecuritySqlInjectionTests
    {
        private readonly IMapper _mapper;

        public SecuritySqlInjectionTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(ApplicationsController).Assembly);
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Update_ShouldNotExecuteSqlInjectionPayload()
        {
            // Arrange
            var db = TestDbContextFactory.Create();

            db.Servers.Add(new Server { Id = 1, Name = "Server A" });
            db.Applications.Add(new Application
            {
                Id = 1,
                Name = "Original",
                Description = "Original",
                ServerId = 1
            });
            db.SaveChanges();

            var controller = new ApplicationsController(db, _mapper);

            // SQL injection payload
            var maliciousName = "Test'); DROP TABLE Applications; --";

            var dto = new ApplicationUpdateDto
            {
                Id = 1,
                Name = maliciousName,
                Description = "Updated",
                ServerId = 1
            };

            // Act
            var result = await controller.Update(1, dto);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            // Ensure the database is still intact
            db.Applications.Count().Should().Be(1);

            // Ensure the payload was stored as plain text (not executed)
            var updated = db.Applications.First();
            updated.Name.Should().Be(maliciousName);
        }
    }
}
