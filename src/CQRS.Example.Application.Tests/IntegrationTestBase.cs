using System;
using System.Net.Http;
using AutoFixture;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CQRS.Example.Application.Tests
{
    public class IntegrationTestBase : IDisposable
    {
        public IntegrationTestBase()
        {
            Factory = new WebApplicationFactory<Startup>();
            Client = Factory.CreateClient();
            Fixture = new Fixture();
        }

        public WebApplicationFactory<Startup> Factory { get; }

        public HttpClient Client { get; }

        public Fixture Fixture { get; }

        public void Dispose()
        {
            Factory.Dispose();
        }
    }
}