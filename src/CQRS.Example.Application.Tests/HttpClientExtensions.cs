using System.Net.Http;
using System.Threading.Tasks;
using CQRS.Example.Server.Customers;
using CQRS.Example.Shared.Http;

namespace CQRS.Example.Application.Tests
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> CreateCustomer(this HttpClient client, CreateCustomerCommand createCustomerCommand)
        {
            var createCustomerRequest = new HttpRequestBuilder()
                .WithMethod(HttpMethod.Post)
                .WithRequestUri("api/customers")
                .WithJsonContent(createCustomerCommand)
                .Build();

            return await client.SendAsync(createCustomerRequest).ConfigureAwait(false);
        }
    }
}