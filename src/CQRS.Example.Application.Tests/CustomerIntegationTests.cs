using System.Diagnostics;
using System.Threading.Tasks;
using AutoFixture;
using CQRS.Example.Application.Tests.Customizations;
using CQRS.Example.Server.Customers;
using LightInject;
using Xunit;

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
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;
            var activity = new Activity("CallToBackend").Start();
            activity.AddBaggage("TEST", "TEST");
            var request = Fixture.Create<CreateCustomerCommand>();

            var response = await Client.CreateCustomer(request);
            activity.Stop();
            response.EnsureSuccessStatusCode();

            foreach (var item in response.Headers)
            {
                var test = item.Key;
            }
        }
    }
}
