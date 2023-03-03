using CA.GraphQL.Application.Common.Exceptions;
using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Domain.Entities;
using CA.GraphQL.Domain.Events;
using MediatR;

namespace CA.GraphQL.Application.TodoItems.Commands.DeleteTodoItem;

public record DeleteTodoItemCommand(int Id) : IRequest<DeleteTodoItemPayload>;

public record DeleteTodoItemPayload(TodoItem TodoItem);

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, DeleteTodoItemPayload>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteTodoItemPayload> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(TodoItem), request.Id);

        _context.TodoItems.Remove(entity);

        entity.AddDomainEvent(new TodoItemDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteTodoItemPayload(entity);
    }
}
