using CA.GraphQL.Domain.Entities;

public class TodoListType : ObjectType<TodoList>
{
    protected override void Configure(IObjectTypeDescriptor<TodoList> descriptor)
    {
        descriptor.Field(x => x.DomainEvents).Ignore();
    }
}
