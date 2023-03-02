using CA.GraphQL.Application.TodoLists.Commands.CreateTodoList;
using CA.GraphQL.Application.TodoLists.Commands.DeleteTodoList;
using CA.GraphQL.Application.TodoLists.Commands.PurgeTodoLists;
using CA.GraphQL.Application.TodoLists.Commands.UpdateTodoList;
using MediatR;

namespace GraphQL.Types;

public class Mutation
{
    public async Task<CreateTodoListPayload> CreateTodoList(CreateTodoListCommand input, [Service] ISender sender) => await sender.Send(input);

    public async Task<DeleteTodoListPayload> DeleteTodoList(DeleteTodoListCommand input, [Service] ISender sender) => await sender.Send(input);

    public async Task<PurgeTodoListsPayload> PurgeTodoList(PurgeTodoListsCommand input, [Service] ISender sender) => await sender.Send(input);

    public async Task<UpdateTodoListPayload> UpdateTodoList(UpdateTodoListCommand input, [Service] ISender sender) => await sender.Send(input);
}