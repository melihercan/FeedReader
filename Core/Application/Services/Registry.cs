using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class Registry : IRegistry
    {
        public List<Feed> Feeds { get; set; } = new List<Feed>();
    }
}
