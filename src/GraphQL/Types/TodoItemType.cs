using CA.GraphQL.Domain.Entities;

public class TodoItemType : ObjectType<TodoItem>
{
    protected override void Configure(IObjectTypeDescriptor<TodoItem> descriptor)
    {
        descriptor.Field(x => x.DomainEvents).Ignore();
    }
}
