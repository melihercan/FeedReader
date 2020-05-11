using Domain.Entities;
using System;
using System.ComponentModel.Design;
using System.Net.Http;
using System.Net.Http.Json;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public class FeedSource : IFeedSource
    {
        private readonly ILogger<FeedSource> _logger;
        private readonly HttpClient _httpClient;


        public FeedSource(/*HttpClient httpClient*/)
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<FeedSource>>();
            _httpClient = ServiceCollectionExtension.ServiceProvider.GetService<HttpClient>();
        }

        public async Task<SyndicationFeed> GetAsync(string url)
        {
            var endpoint = _httpClient.BaseAddress.AbsoluteUri + "api/SyndicationFeed";
            Console.WriteLine($"endpoint: {endpoint}");

            var uriBuilder = new UriBuilder(new Uri(endpoint));
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[Feed.Url] = url;
            uriBuilder.Query = query.ToString();
            var uri = uriBuilder.ToString();
            return await _httpClient.GetFromJsonAsync<SyndicationFeed>(uri);
        }
    }
}
