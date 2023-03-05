using CA.GraphQL.Application.Common.Exceptions;
using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Domain.Entities;
using MediatR;

namespace CA.GraphQL.Application.TodoLists.Commands.UpdateTodoList;

public record UpdateTodoListCommand : IRequest<UpdateTodoListPayload>
{
    public int Id { get; init; }

    public string? Title { get; init; }
}

public record UpdateTodoListPayload(TodoList TodoList);

public class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand, UpdateTodoListPayload>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateTodoListPayload> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoLists
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(TodoList), request.Id);

        entity.Title = request.Title;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateTodoListPayload(entity);
    }
}

