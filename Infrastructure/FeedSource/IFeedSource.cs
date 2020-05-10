using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IFeedSource
    {
        Task<SyndicationFeed> GetAsync(string url);
    }
}
