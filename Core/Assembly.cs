using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public static class Assembly
    {
        public static System.Reflection.Assembly Value => typeof(Assembly).Assembly;
    }
}
