using Entities.Models;
using Interactors.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gateways.FeedRepository.Lib
{
    public class FeedRepository : IFeedRepository
    {
        public Task Create(FeedUrl feedUrl)
        {
            throw new NotImplementedException();
        }
    }
}
