using Entities.Models;
using Interactors.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gateways.Repository
{
    public class FeedRepository : IFeedRepository
    {
        public FeedRepository()
        {
        }

        public Task Create(FeedUrl feedUrl)
        {
            throw new NotImplementedException();
        }
    }
}
