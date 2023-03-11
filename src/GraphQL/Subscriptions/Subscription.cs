using GraphQL.Mutations;

namespace GraphQL.Subscriptions;

[SubscriptionType]
public class Subscription
{
    [Subscribe]
    [Topic(nameof(TodoItemMutation.CreateTodoItem))]
    public TodoItemCreatedPayload OnTodoItemCreated([EventMessage] TodoItemCreatedPayload todoItem)
    {
        return todoItem;
    }
}
