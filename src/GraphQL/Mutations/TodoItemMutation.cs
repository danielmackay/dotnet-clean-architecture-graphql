using CA.GraphQL.Application.TodoItems.Commands.CompleteTodoItem;
using CA.GraphQL.Application.TodoItems.Commands.CreateTodoItem;
using CA.GraphQL.Application.TodoItems.Commands.DeleteTodoItem;
using CA.GraphQL.Application.TodoItems.Commands.PurgeTodoItems;
using CA.GraphQL.Application.TodoItems.Commands.UpdateTodoItem;
using CA.GraphQL.Application.TodoItems.Commands.UpdateTodoItemDetail;
using MediatR;

namespace GraphQL.Mutations;

[MutationType]
public class TodoItemMutation
{
    public async Task<CreateTodoItemPayload> CreateTodoItem(CreateTodoItemCommand input, [Service] ISender sender) => await sender.Send(input);

    public async Task<DeleteTodoItemPayload> DeleteTodoItem(DeleteTodoItemCommand input, [Service] ISender sender) => await sender.Send(input);

    public async Task<UpdateTodoItemPayload> UpdateTodoItem(UpdateTodoItemCommand input, [Service] ISender sender) => await sender.Send(input);

    public async Task<UpdateTodoItemDetailPayload> UpdateTodoItemDetail(UpdateTodoItemDetailCommand input, [Service] ISender sender) => await sender.Send(input);

    public async Task<CompleteTodoItemPayload> CompleteTodoItem(CompleteTodoItemCommand input, [Service] ISender sender) => await sender.Send(input);

    public async Task<PurgeTodoItemsPayload> PurgeTodoItems(PurgeTodoItemsCommand input, [Service] ISender sender) => await sender.Send(input);
}