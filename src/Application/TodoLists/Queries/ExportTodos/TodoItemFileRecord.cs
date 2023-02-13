using CA.GraphQL.Application.Common.Mappings;
using CA.GraphQL.Domain.Entities;

namespace CA.GraphQL.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
