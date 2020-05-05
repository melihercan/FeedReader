using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Application.Helpers
{
    public class Utils
    {
        static public string GetCurrentMethod([CallerMemberName] string callerName = "")
        {
            return callerName;
        }
    }
}
