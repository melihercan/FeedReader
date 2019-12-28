using FeedReader.Application.Common.Interfaces;
using System;

namespace FeedReader.WebUI.IntegrationTests
{
    public class TestDateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
