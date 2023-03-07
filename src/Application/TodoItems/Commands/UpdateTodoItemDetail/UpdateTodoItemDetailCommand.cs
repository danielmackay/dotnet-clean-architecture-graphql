using CA.GraphQL.Application.Common.Exceptions;
using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Domain.Entities;
using CA.GraphQL.Domain.Enums;
using MediatR;

namespace CA.GraphQL.Application.TodoItems.Commands.UpdateTodoItemDetail;

public record UpdateTodoItemDetailCommand : IRequest<UpdateTodoItemDetailPayload>
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }
}

public record UpdateTodoItemDetailPayload(TodoItem TodoItem);

public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand, UpdateTodoItemDetailPayload>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateTodoItemDetailPayload> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

#pragma warning disable IDE0270 // Use coalesce expression
        if (entity == null)
            throw new NotFoundException(nameof(TodoItem), request.Id);
#pragma warning restore IDE0270 // Use coalesce expression

        entity.ListId = request.ListId;
        entity.Priority = request.Priority;
        entity.Note = request.Note;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateTodoItemDetailPayload(entity);
    }
}
