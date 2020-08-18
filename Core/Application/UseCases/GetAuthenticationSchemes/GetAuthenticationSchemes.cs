using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class GetAuthenticationSchemes : IRequest<Result<string[]>>
    {
    }
}
