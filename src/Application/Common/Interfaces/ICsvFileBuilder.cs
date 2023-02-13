using CA.GraphQL.Application.TodoLists.Queries.ExportTodos;

namespace CA.GraphQL.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
