using Ardalis.Result;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class Login : IRequest<Result<Token>>
    {
        public User User { get; set; }
        public string Scheme { get; set; }
    }
}
