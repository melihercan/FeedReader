using WebWindows.Blazor;
using System;

namespace DesktopUi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ComponentsDesktop.Run<Startup>("FeedReader", "wwwroot/index.html");
        }

    }
}
