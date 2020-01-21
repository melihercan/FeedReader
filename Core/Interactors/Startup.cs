using Interactors.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interactors
{
    public class Startup
    {
        public static IFeedRepository Repository { get; private set; }
        public static IIdentity Identity { get; private set; }

        public Startup(IFeedRepository repository, IIdentity identity)
        {
            Repository = repository;
            Identity = identity;

        }
    }
}
