using CA.GraphQL.Application.Common.Exceptions;
using CA.GraphQL.Application.TodoLists.Commands.CreateTodoList;
//using FluentValidation;
using MediatR;

namespace GraphQL.Types;

public class Mutation
{
    public async Task<int> CreateTodoList(CreateTodoListCommand command, [Service] ISender sender) => await sender.Send(command);
}

public class MutationType : ObjectType<Mutation>
{
    protected override void Configure(
        IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor
          .Field(f => f.CreateTodoList(default!, default!))
          .Error<ValidationException>();
    }
}