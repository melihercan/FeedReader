using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interactors.Interfaces
{
    public interface IFeedRepository
    {
        Task Create(FeedUrl feedUrl);
    }
}
