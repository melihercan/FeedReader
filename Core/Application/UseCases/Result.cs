using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class Result<T> where T : class
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public T Value { get; set; }



        public Result(T value = null, bool success = true, string error="")
        {
            Success = success;
            Error = error;
            Value = value;
        }
    }
}
