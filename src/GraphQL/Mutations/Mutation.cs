using CA.GraphQL.Application.TodoItems.Commands.CompleteTodoItem;
using CA.GraphQL.Application.TodoItems.Commands.CreateTodoItem;
using CA.GraphQL.Application.TodoItems.Commands.DeleteTodoItem;
using CA.GraphQL.Application.TodoItems.Commands.UpdateTodoItem;
using CA.GraphQL.Application.TodoItems.Commands.UpdateTodoItemDetail;
using CA.GraphQL.Application.TodoLists.Commands.CreateTodoList;
using CA.GraphQL.Application.TodoLists.Commands.DeleteTodoList;
using CA.GraphQL.Application.TodoLists.Commands.PurgeTodoLists;
using CA.GraphQL.Application.TodoLists.Commands.UpdateTodoList;
using MediatR;

namespace GraphQL.Mutations;

public class Mutation
{
    public async Task<CreateTodoListPayload> CreateTodoList(CreateTodoListCommand input, [Service] ISender sender) => await sender.Send(input);

    public async Task<DeleteTodoListPayload> DeleteTodoList(DeleteTodoListCommand input, [Service] ISender sender) => await sender.Send(input);

    public async Task<PurgeTodoListsPayload> PurgeTodoList([Service] ISender sender) => await sender.Send(new PurgeTodoListsCommand());

    public async Task<UpdateTodoListPayload> UpdateTodoList(UpdateTodoListCommand input, [Service] ISender sender) => await sender.Send(input);

    public async Task<CreateTodoItemPayload> CreateTodoItem(CreateTodoItemCommand input, [Service] ISender sender) => await sender.Send(input);

    public async Task<DeleteTodoItemPayload> DeleteTodoItem(DeleteTodoItemCommand input, [Service] ISender sender) => await sender.Send(input);

    public async Task<UpdateTodoItemPayload> UpdateTodoItem(UpdateTodoItemCommand input, [Service] ISender sender) => await sender.Send(input);

    public async Task<UpdateTodoItemDetailPayload> UpdateTodoItemDetail(UpdateTodoItemDetailCommand input, [Service] ISender sender) => await sender.Send(input);

    public async Task<CompleteTodoItemPayload> CompleteTodoItem(CompleteTodoItemCommand input, [Service] ISender sender) => await sender.Send(input);
}