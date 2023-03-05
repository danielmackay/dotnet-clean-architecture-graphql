using CA.GraphQL.Application.Common.Exceptions;
using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Domain.Entities;
using MediatR;

namespace CA.GraphQL.Application.TodoItems.Commands.CompleteTodoItem;

public record CompleteTodoItemCommand : IRequest<CompleteTodoItemPayload>
{
    public int Id { get; init; }

    public bool Done { get; init; }
}

public record CompleteTodoItemPayload(TodoItem TodoItem);

public class CompleteTodoItemCommandHandler : IRequestHandler<CompleteTodoItemCommand, CompleteTodoItemPayload>
{
    private readonly IApplicationDbContext _context;

    public CompleteTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CompleteTodoItemPayload> Handle(CompleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

#pragma warning disable IDE0270 // Use coalesce expression
        if (entity == null)
            throw new NotFoundException(nameof(TodoItem), request.Id);
#pragma warning restore IDE0270 // Use coalesce expression

        entity.Done = request.Done;
        await _context.SaveChangesAsync(cancellationToken);

        return new CompleteTodoItemPayload(entity);
    }
}
