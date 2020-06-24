using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFeedRepository
    {
        Task<IEnumerable<FeedChannel>> GetFeedChannelsAsync();

        Task<FeedChannel> GetFeedChannelAsync(int id);

        Task AddFeedChannelAsync(FeedChannel feedChannel);
        
        Task RemoveFeedChannelAsync(int id);

        Task UpdateFeedChannelAsync(int id, FeedChannel feedChannel);
    }
}
