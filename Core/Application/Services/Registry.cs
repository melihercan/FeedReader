using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class Registry : IRegistry
    {
        public IEnumerable<Feed> Feeds { get; set; }
    }
}
