using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IRegistry
    {
        public List<Feed> Feeds { get; set; }
    }
}
