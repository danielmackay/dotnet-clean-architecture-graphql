using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Domain.Entities;
using CA.GraphQL.Domain.Events;
using MediatR;

namespace CA.GraphQL.Application.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand : IRequest<CreateTodoItemPayload>
{
    public int ListId { get; init; }

    public string? Title { get; init; }
}

public record CreateTodoItemPayload(TodoItem TodoItem);

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, CreateTodoItemPayload>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CreateTodoItemPayload> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoItem
        {
            ListId = request.ListId,
            Title = request.Title,
            Done = false
        };

        entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        _context.TodoItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return new CreateTodoItemPayload(entity);
    }
}
