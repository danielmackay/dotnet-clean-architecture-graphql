using CA.GraphQL.Application.Common.Interfaces;
//using CA.GraphQL.Application.Common.Security;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace CA.GraphQL.Application.TodoItems.Commands.PurgeTodoItems;

//[Authorize(Roles = "Administrator")]
//[Authorize(Policy = "CanPurge")]
public record PurgeTodoItemsCommand(int ListId) : IRequest<PurgeTodoItemsPayload>;

public record PurgeTodoItemsPayload(int Removed);

public class PurgeTodoItemsCommandHandler : IRequestHandler<PurgeTodoItemsCommand, PurgeTodoItemsPayload>
{
    private readonly IApplicationDbContext _context;

    public PurgeTodoItemsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PurgeTodoItemsPayload> Handle(PurgeTodoItemsCommand request, CancellationToken cancellationToken)
    {
        var rowsRemoved = await _context.TodoItems.Where(ti => ti.ListId == request.ListId).ExecuteDeleteAsync(cancellationToken);

        return new PurgeTodoItemsPayload(rowsRemoved);
    }
}
