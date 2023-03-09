using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Domain.Entities;

namespace CA.GraphQL.Infrastructure.Persistence.Repositories;

public class TodoItemRepository : ITodoItemRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TodoItemRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TodoItem> GetAll() => _dbContext.TodoItems;

    public IQueryable<TodoItem> GetAll(int todoItemId) => _dbContext.TodoItems.Where(ti => ti.Id == todoItemId);
}
