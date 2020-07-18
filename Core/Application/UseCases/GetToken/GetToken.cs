﻿using Ardalis.Result;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases
{
    public class GetToken : IRequest<Result<Token>>
    {
    }
}
