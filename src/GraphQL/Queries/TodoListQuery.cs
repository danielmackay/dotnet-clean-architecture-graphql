using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Domain.Entities;
using CA.GraphQL.Infrastructure.Persistence;
using GreenDonut.Data;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using HotChocolate.Types.Pagination;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Queries;

[QueryType]
public static class TodoListQuery
{
    public static async Task<TodoList?> GetTodoListById(
        int id,
        QueryContext<TodoList> query,
        ApplicationDbContext dbContext,
        CancellationToken ct)
        => await dbContext.TodoLists
            .With(query)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public static async Task<Connection<TodoList>> GetTodoLists(
        PagingArguments pagingArgs,
        QueryContext<TodoList> query,
        ApplicationDbContext dbContext,
        CancellationToken ct)
        => await dbContext.TodoLists
            .OrderBy(ti => ti.Title)
            .With(query)
            .AsNoTracking()
            .ToPageAsync(pagingArgs, ct)
            .ToConnectionAsync();
}

/// <summary>
/// Restricts the fields that can be used for filtering TodoLists
/// </summary>
public class TodoListFilter : FilterInputType<TodoList>
{
    protected override void Configure(IFilterInputTypeDescriptor<TodoList> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(ti => ti.Title);
        descriptor.Field(ti => ti.Id);
    }
}

/// <summary>
/// Restricts the fields that can be used for sorting TodoLists
/// </summary>
public class TodoListSorting : SortInputType<TodoList>
{
    protected override void Configure(ISortInputTypeDescriptor<TodoList> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(ti => ti.Title);
    }
}