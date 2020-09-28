using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public static class Registry
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static string ServerUrl { get; set; }

    }
}
