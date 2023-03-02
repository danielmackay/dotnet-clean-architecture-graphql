using CA.GraphQL.Domain.Entities;

namespace GraphQL.Types;

public class TodoItemType : ObjectType<TodoItem>
{
    protected override void Configure(IObjectTypeDescriptor<TodoItem> descriptor)
    {
        // Ignoring Fields
        descriptor.Field(x => x.DomainEvents).Ignore();
        //descriptor.Field(x => x.Created).Ignore();
        //descriptor.Field(x => x.CreatedBy).Ignore();

        // Renaming Fields
        //descriptor.Field(x => x.PlainText).Name("description");


        // Adding Fields
        descriptor.Field("ssw").Resolve("Dan Was Here");
    }
}