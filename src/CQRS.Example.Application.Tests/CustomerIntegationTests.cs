using System.Net.Http;
using CQRS.Example.Shared.Http;
using LightInject;
using Xunit;
using AutoFixture;
using System.Threading.Tasks;
using CQRS.Example.Server.Customers;
using CQRS.Example.Application.Tests.Customizations;
using System.ComponentModel.DataAnnotations;

namespace CQRS.Example.Application.Tests
{
    public class CustomerIntegrationTests : IntegrationTestBase
    {
        public CustomerIntegrationTests()
        {
            Fixture.Customizations.Add(new MailAddressCustomization());
        }

        [Fact]
        public async Task ShouldCreateCustomer()
        {
            var request = Fixture.Create<CreateCustomerCommand>();
            var response = await Client.CreateCustomer(request);
            response.EnsureSuccessStatusCode();
        }
    }
}
