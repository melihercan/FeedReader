using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases
{
    public class AddFeedHandler : IRequestHandler<AddFeed>
    {
        private readonly IRegistry _registry;

        public AddFeedHandler()
        {
            Console.WriteLine("AddFeedHandler constructor...");
            _registry = ModuleInitializer.ServiceProvider.GetService<IRegistry>();
        }

        public async Task<Unit> Handle(AddFeed request, CancellationToken cancellationToken)
        {
            Console.WriteLine("AddFeedHander Handle...");
            await Task.CompletedTask;
            return new Unit();
        }
    }
}
