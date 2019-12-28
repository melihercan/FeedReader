using FeedReader.Application.Common.Exceptions;
using FeedReader.Application.Common.Interfaces;
using FeedReader.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FeedReader.Application.TodoItems.Commands.DeleteTodoItem
{
    public class DeleteTodoItemCommand : IRequest
    {
        public long Id { get; set; }

        public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
        {
            private readonly IApplicationDbContext _context;

            public DeleteTodoItemCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.TodoItems.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(TodoItem), request.Id);
                }

                _context.TodoItems.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
