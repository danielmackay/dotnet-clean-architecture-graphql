using CA.GraphQL.Application.TodoLists.Commands.CreateTodoList;
using CA.GraphQL.Application.TodoLists.Commands.DeleteTodoList;
using CA.GraphQL.Application.TodoLists.Commands.PurgeTodoLists;
using CA.GraphQL.Application.TodoLists.Commands.UpdateTodoList;
using MediatR;

namespace GraphQL.Mutations;

[MutationType]
public class TodoListMutation
{
    public async Task<CreateTodoListPayload> CreateTodoList(CreateTodoListCommand input, [Service] ISender sender) => await sender.Send(input);

    public async Task<DeleteTodoListPayload> DeleteTodoList(DeleteTodoListCommand input, [Service] ISender sender) => await sender.Send(input);

    public async Task<PurgeTodoListsPayload> PurgeTodoList([Service] ISender sender) => await sender.Send(new PurgeTodoListsCommand());

    public async Task<UpdateTodoListPayload> UpdateTodoList(UpdateTodoListCommand input, [Service] ISender sender) => await sender.Send(input);
}
