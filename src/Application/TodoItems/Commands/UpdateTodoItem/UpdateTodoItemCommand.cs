using CA.GraphQL.Application.Common.Exceptions;
using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Domain.Entities;
using MediatR;

namespace CA.GraphQL.Application.TodoItems.Commands.UpdateTodoItem;

public record UpdateTodoItemCommand : IRequest<UpdateTodoItemPayload>
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}

public record UpdateTodoItemPayload(TodoItem TodoItem);

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, UpdateTodoItemPayload>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateTodoItemPayload> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(TodoItem), request.Id);

        entity.Title = request.Title;
        entity.Done = request.Done;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateTodoItemPayload(entity);
    }
}
