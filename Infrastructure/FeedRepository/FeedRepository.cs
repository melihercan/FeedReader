using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace Infrastructure
{
    public class FeedRepository : IFeedRepository
    {
        private readonly ILogger<FeedRepository> _logger;
        private readonly HttpClient _httpClient;

        public FeedRepository()
        {
            _logger = ServiceCollectionExtension.ServiceProvider.GetService<ILogger<FeedRepository>>();
            _httpClient = ServiceCollectionExtension.ServiceProvider.GetService<HttpClient>();
        }

        public async Task<IEnumerable<FeedChannel>> GetFeedChannelsAsync()
        {
            return await _httpClient.GetFromJsonAsync<FeedChannel[]>("api/FeedChannels");
        }

        public async Task AddFeedChannelAsync(FeedChannel feedChannel)
        {
            await _httpClient.PostAsJsonAsync("api/FeedChannels", feedChannel);
        }

        public async Task RemoveFeedChannelAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/FeedChannels/{id}");
        }

        public async Task UpdateFeedChannelAsync(int id, FeedChannel feedChannel)
        {
            await _httpClient.PutAsJsonAsync($"api/FeedChannels/{id}", feedChannel);
        }
    }
}
