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
public static class TodoItemQuery
{
    public static async Task<TodoItem?> GetTodoItemById(
        int id,
        QueryContext<TodoItem> query,
        ApplicationDbContext dbContext,
        CancellationToken ct)
        => await dbContext.TodoItems
            .With(query)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public static async Task<Connection<TodoItem>> GetTodoItems(
        PagingArguments pagingArgs,
        QueryContext<TodoItem> query,
        ApplicationDbContext dbContext,
        CancellationToken ct)
        => await dbContext.TodoItems
            .OrderBy(ti => ti.Title)
            .With(query)
            .AsNoTracking()
            .ToPageAsync(pagingArgs, ct)
            .ToConnectionAsync();
}

/// <summary>
/// Restricts the fields that can be used for filtering TodoItems
/// </summary>
public class TodoItemFilter : FilterInputType<TodoItem>
{
    protected override void Configure(IFilterInputTypeDescriptor<TodoItem> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(ti => ti.Title);
        descriptor.Field(ti => ti.Done);
        descriptor.Field(ti => ti.Id);
    }
}

/// <summary>
/// Restricts the fields that can be used for sorting TodoItems
/// </summary>
public class TodoItemSorting : SortInputType<TodoItem>
{
    protected override void Configure(ISortInputTypeDescriptor<TodoItem> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(ti => ti.Title);
    }
}