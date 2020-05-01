using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Core.UseCases
{
    public class AddFeedHandler : IRequestHandler<AddFeed>
    {
        private readonly IRegistry _registry;

        public AddFeedHandler()
        {
            Console.WriteLine("AddFeed constructor...");
            _registry = ModuleInitializer.ServiceProvider.GetService<IRegistry>();
        }

        public async Task<Unit> Handle(AddFeed request, CancellationToken cancellationToken)
        {
            Console.WriteLine("AddFeed handler...");
            await Task.CompletedTask;
            return new Unit();
        }
    }
}
