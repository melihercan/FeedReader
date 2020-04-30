using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;

namespace Core.UseCases
{
    public class Init : IRequest<IObservable<FeedChannel>>
    {
    }
}
