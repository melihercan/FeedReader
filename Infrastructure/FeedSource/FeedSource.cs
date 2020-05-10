using Domain.Entities;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure
{
    public class FeedSource : IFeedSource
    {
        private readonly HttpClient _httpClient;


        public FeedSource(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SyndicationFeed> GetAsync(string url)
        {
            var uriBuilder = new UriBuilder(new Uri("SyndicationFeed"));
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[Feed.Url] = url;
            uriBuilder.Query = query.ToString();
            var uri = uriBuilder.ToString();
            return await _httpClient.GetFromJsonAsync<SyndicationFeed>(uri);
        }
    }
}
