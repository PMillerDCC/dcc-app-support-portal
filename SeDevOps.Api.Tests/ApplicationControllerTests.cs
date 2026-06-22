using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeDevOps.Api.Controllers;
using SeDevOps.Api.Dtos;
using SeDevOps.Data;
using SeDevOps.Data.Entities;
using Xunit;

namespace SeDevOps.Api.Tests
{
    public class ApplicationsControllerTests
    {
        private readonly IMapper _mapper;

        public ApplicationsControllerTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(ApplicationsController).Assembly);
            });

            _mapper = config.CreateMapper();
        }

        // -----------------------------
        // UPDATE TESTS
        // -----------------------------

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            var db = TestDbContextFactory.Create();
            var controller = new ApplicationsController(db, _mapper);

            var dto = new ApplicationUpdateDto
            {
                Id = 2,
                Name = "Test",
                Description = "Test",
                ServerId = 1
            };

            var result = await controller.Update(1, dto);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenAppDoesNotExist()
        {
            var db = TestDbContextFactory.Create();
            var controller = new ApplicationsController(db, _mapper);

            var dto = new ApplicationUpdateDto
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                ServerId = 1
            };

            var result = await controller.Update(1, dto);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenSuccessful()
        {
            var db = TestDbContextFactory.Create();

            db.Servers.Add(new Server { Id = 1, Name = "Server A" });
            db.Applications.Add(new Application
            {
                Id = 1,
                Name = "Old",
                Description = "Old",
                ServerId = 1
            });
            db.SaveChanges();

            var controller = new ApplicationsController(db, _mapper);

            var dto = new ApplicationUpdateDto
            {
                Id = 1,
                Name = "Updated",
                Description = "Updated",
                ServerId = 1
            };

            var result = await controller.Update(1, dto);

            result.Should().BeOfType<NoContentResult>();

            var updated = db.Applications.First();
            updated.Name.Should().Be("Updated");
            updated.Description.Should().Be("Updated");
        }

        // -----------------------------
        // CREATE TESTS
        // -----------------------------

        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WhenSuccessful()
        {
            var db = TestDbContextFactory.Create();
            var controller = new ApplicationsController(db, _mapper);

            db.Servers.Add(new Server { Id = 1, Name = "Server A" });
            db.SaveChanges();

            var dto = new ApplicationCreateDto
            {
                Name = "New App",
                Description = "Desc",
                ServerId = 1
            };

            var result = await controller.Create(dto);

            result.Should().BeOfType<CreatedAtActionResult>();
        }

        // -----------------------------
        // DELETE TESTS
        // -----------------------------

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenAppDoesNotExist()
        {
            var db = TestDbContextFactory.Create();
            var controller = new ApplicationsController(db, _mapper);

            var result = await controller.Delete(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccessful()
        {
            var db = TestDbContextFactory.Create();

            db.Applications.Add(new Application
            {
                Id = 1,
                Name = "App",
                Description = "Desc",
                ServerId = 1
            });
            db.SaveChanges();

            var controller = new ApplicationsController(db, _mapper);

            var result = await controller.Delete(1);

            result.Should().BeOfType<NoContentResult>();
            db.Applications.Count().Should().Be(0);
        }
    }
}
