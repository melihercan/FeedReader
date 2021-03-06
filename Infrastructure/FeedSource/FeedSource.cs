﻿using Domain.Entities;
using System;
using System.ComponentModel.Design;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Application.Interfaces;
using Shared;

namespace Infrastructure
{
    public class FeedSource : IFeedSource
    {
        private readonly ILogger<FeedSource> _logger;
        private readonly HttpClient _httpClient;

        public FeedSource(/*HttpClient httpClient*/)
        {
            _logger = Registry.ServiceProvider.GetService<ILogger<FeedSource>>();
            _httpClient = Registry.ServiceProvider.GetService<HttpClient>();
        }

        public async Task<FeedChannel> GetAsync(string url)
        {
            var endpoint = _httpClient.BaseAddress.AbsoluteUri + "api/FeedSource";
            Console.WriteLine($"endpoint: {endpoint}");

            var uriBuilder = new UriBuilder(new Uri(endpoint));
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[Feed.Url] = url;
            uriBuilder.Query = query.ToString();
            var uri = uriBuilder.ToString();
            var feedChannel = await _httpClient.GetFromJsonAsync<FeedChannel>(uri);
            return feedChannel;
        }
    }
}
