using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Domain.Entities;
using MediatR;

namespace CA.GraphQL.Application.TodoLists.Commands.CreateTodoList;

public record CreateTodoListCommand : IRequest<CreateTodoListPayload>
{
    public string? Title { get; init; }
}

public record CreateTodoListPayload(TodoList TodoList);

public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, CreateTodoListPayload>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CreateTodoListPayload> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoList();

        entity.Title = request.Title;

        _context.TodoLists.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return new CreateTodoListPayload(entity);
    }
}

