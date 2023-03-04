using BlazorUI.Models;

namespace BlazorUI.Services;

public class TodoListState
{
    private readonly StorageService _storage;

    public TodoListState(StorageService sessionStorage)
    {
        _storage = sessionStorage;
    }

    public async Task<List<TodoItem>?> Get()
    {
        return await _storage.GetItemAsync<List<TodoItem>>(StorageKeys.TodoList);
    }

    public async Task Set(List<TodoItem> todoItems)
    {
        await _storage.SetItemAsync(StorageKeys.TodoList, todoItems);
    }
}
