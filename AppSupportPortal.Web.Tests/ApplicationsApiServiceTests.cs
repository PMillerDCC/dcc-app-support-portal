using AppSupportPortal.Web.Models;
using AppSupportPortal.Web.Models.Dtos;
using AppSupportPortal.Web.Services;
using FluentAssertions;
using System.Net;
using Xunit;

namespace AppSupportPortal.Web.Tests
{
    public class ApplicationsApiServiceTests
    {
        // -----------------------------
        // UPDATE TESTS
        // -----------------------------

        [Fact]
        public async Task UpdateAsync_ReturnsTrue_OnSuccess()
        {
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);
            var handler = new MockHttpMessageHandler(response);
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://localhost")
            };

            var service = new ApplicationsApiService(client);

            var vm = new ApplicationViewModel
            {
                Id = 1,
                Name = "Test",
                Description = "Desc",
                ServerId = 2
            };

            var result = await service.UpdateAsync(vm);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse_OnFailure()
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            var handler = new MockHttpMessageHandler(response);
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://localhost")
            };

            var service = new ApplicationsApiService(client);

            var vm = new ApplicationViewModel
            {
                Id = 1,
                Name = "Test",
                Description = "Desc",
                ServerId = 2
            };

            var result = await service.UpdateAsync(vm);

            result.Should().BeFalse();
        }

        // -----------------------------
        // CREATE TESTS
        // -----------------------------

        [Fact]
        public async Task CreateAsync_ReturnsTrue_OnSuccess()
        {
            var response = new HttpResponseMessage(HttpStatusCode.Created);
            var handler = new MockHttpMessageHandler(response);
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://localhost")
            };

            var service = new ApplicationsApiService(client);

            var vm = new ApplicationViewModel
            {
                Name = "New App",
                Description = "Desc",
                ServerId = 1
            };

            var result = await service.CreateAsync(vm);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CreateAsync_ReturnsFalse_OnFailure()
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            var handler = new MockHttpMessageHandler(response);
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://localhost")
            };

            var service = new ApplicationsApiService(client);

            var vm = new ApplicationViewModel
            {
                Name = "New App",
                Description = "Desc",
                ServerId = 1
            };

            var result = await service.CreateAsync(vm);

            result.Should().BeFalse();
        }

        // -----------------------------
        // DELETE TESTS
        // -----------------------------

        [Fact]
        public async Task DeleteAsync_ReturnsNull_OnSuccess()
        {
            var response = new HttpResponseMessage(HttpStatusCode.NoContent);
            var handler = new MockHttpMessageHandler(response);
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://localhost")
            };

            var service = new ApplicationsApiService(client);

            var result = await service.DeleteAsync(1);

            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_ReturnsErrorMessage_OnFailure()
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Error deleting")
            };

            var handler = new MockHttpMessageHandler(response);
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://localhost")
            };

            var service = new ApplicationsApiService(client);

            var result = await service.DeleteAsync(1);

            result.Should().Be("Error deleting");
        }
    }
}