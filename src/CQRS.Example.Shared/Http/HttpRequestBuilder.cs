using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace CQRS.Example.Shared.Http
{
    public class HttpRequestBuilder
    {
        private string requestUri = string.Empty;

        private const string defaultAccepttHeader = "application/json";

        private HttpRequestMessage requestMessage = new HttpRequestMessage();

        private Dictionary<string, string> queryParameters = new Dictionary<string, string>();

        public HttpRequestBuilder WithMethod(HttpMethod method)
        {
            requestMessage.Method = method;
            return this;
        }

        public HttpRequestBuilder WithRequestUri(string requestUri)
        {
            this.requestUri = requestUri;
            return this;
        }

        public HttpRequestBuilder AddQueryParameter(string name, object value)
        {
            var test = value.ToString();

            this.queryParameters[name] = test ??= string.Empty;
            return this;
        }

        public HttpRequestBuilder AddHeader(string name, string value)
        {
            requestMessage.Headers.Add(name, value);
            return this;
        }

        public HttpRequestBuilder WithBearerToken(string bearerToken)
        {
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            return this;
        }

        public HttpRequestBuilder WithAcceptHeader(string acceptHeader)
        {
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptHeader));
            return this;
        }

        public HttpRequestBuilder WithJsonContent<T>(T content) where T : class
        {
            requestMessage.Content = new JsonContent(content);
            return this;
        }

        public HttpRequestMessage Build()
        {
            var queryString = BuildQueryString();
            if (!string.IsNullOrWhiteSpace(queryString))
            {
                requestUri = $"{requestUri}?{queryString}$";
            }

            if (requestMessage.Headers.Accept.Count == 0)
            {
                WithAcceptHeader(defaultAccepttHeader);
            }

            requestMessage.RequestUri = new Uri(requestUri, UriKind.Relative);
            return requestMessage;
        }

        private string BuildQueryString()
        {
            if (queryParameters.Count == 0)
            {
                return string.Empty;
            }
            var encoder = UrlEncoder.Default;
            return queryParameters
                .Select(kvp => $"{encoder.Encode(kvp.Key)}={encoder.Encode(kvp.Value)}")
                .Aggregate((current, next) => $"{current}&{next}");
        }
    }

    public class JsonContent : StringContent
    {
        public JsonContent(object value)
            : base(JsonSerializer.Serialize(value), Encoding.UTF8,
            "application/json")
        {
        }
    }


    public static class HttpResponseMessageResultExtensions
    {
        public static async Task<T?> ContentAs<T>(this HttpResponseMessage response) where T : class
        {
            var data = await response.Content.ReadAsStringAsync();
            return string.IsNullOrEmpty(data) ?
                            default(T) :
                            JsonSerializer.Deserialize<T>(data);
        }
    }
}