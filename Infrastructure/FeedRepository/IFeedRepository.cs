using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IFeedRepository
    {
        Task<IEnumerable<FeedChannel>> GetFeedChannelsAsync();
        
        Task AddFeedChannelAsync(FeedChannel feedChannel);
        
        Task RemoveFeedChannelAsync(int id);
    }
}
