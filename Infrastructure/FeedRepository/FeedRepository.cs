﻿using Domain.Entities;
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

        public Task Create(FeedChannel feedChannel)
        {
            throw new NotImplementedException();
        }

    }
}
