using CA.GraphQL.Application.Common.Exceptions;
using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CA.GraphQL.Application.TodoLists.Commands.DeleteTodoList;

public record DeleteTodoListCommand(int Id) : IRequest<DeleteTodoListPayload>;

public record DeleteTodoListPayload(TodoList TodoList);

public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand, DeleteTodoListPayload>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteTodoListPayload> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoLists
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(TodoList), request.Id);

        _context.TodoLists.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteTodoListPayload(entity);
    }
}

