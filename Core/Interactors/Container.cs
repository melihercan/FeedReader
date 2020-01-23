using Interactors.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interactors
{
    public class Container
    {
        public static IFeedRepository Repository { get; private set; }
        public static IIdentity Identity { get; private set; }

        public static void Init(IFeedRepository repository, IIdentity identity)
        {
            Repository = repository;
            Identity = identity;

        }

    }
}
