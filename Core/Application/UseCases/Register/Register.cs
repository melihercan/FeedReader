using Ardalis.Result;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class Register : IRequest<Result<object>>
    {
        public User User { get; set; }
    }
}
