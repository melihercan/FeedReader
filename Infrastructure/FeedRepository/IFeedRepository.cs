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
        
        Task Create(FeedChannel feedChannel);
    }
}
