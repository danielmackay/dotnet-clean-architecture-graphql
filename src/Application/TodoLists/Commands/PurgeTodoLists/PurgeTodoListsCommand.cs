using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Application.Common.Security;
using MediatR;

namespace CA.GraphQL.Application.TodoLists.Commands.PurgeTodoLists;

[Authorize(Roles = "Administrator")]
[Authorize(Policy = "CanPurge")]
public record PurgeTodoListsCommand : IRequest<PurgeTodoListsPayload>;

public record PurgeTodoListsPayload(bool Success);

public class PurgeTodoListsCommandHandler : IRequestHandler<PurgeTodoListsCommand, PurgeTodoListsPayload>
{
    private readonly IApplicationDbContext _context;

    public PurgeTodoListsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PurgeTodoListsPayload> Handle(PurgeTodoListsCommand request, CancellationToken cancellationToken)
    {
        _context.TodoLists.RemoveRange(_context.TodoLists);

        await _context.SaveChangesAsync(cancellationToken);

        return new PurgeTodoListsPayload(true);
    }
}

