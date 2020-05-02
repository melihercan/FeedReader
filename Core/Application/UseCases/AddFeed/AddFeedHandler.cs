using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Domain.Entities;

namespace Application.UseCases
{
    public class AddFeedHandler : IRequestHandler<AddFeed, FeedChannel>
    {
        private readonly IRegistry _registry;

        public AddFeedHandler()
        {
            Console.WriteLine("AddFeedHandler constructor...");
            _registry = ModuleInitializer.ServiceProvider.GetService<IRegistry>();
        }

        public async Task<FeedChannel> Handle(AddFeed request, CancellationToken cancellationToken)
        {
            Console.WriteLine("AddFeedHander Handle...");

            var validator = new AddFeedValidator();
            var validationResult = validator.Validate(request);
            if(validationResult.IsValid)
            {

            }

            await Task.CompletedTask;
            return null;
        }
    }
}
