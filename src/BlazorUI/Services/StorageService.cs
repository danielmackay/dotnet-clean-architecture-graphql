using Microsoft.JSInterop;
using System.Text.Json;

namespace BlazorUI.Services;

public class StorageService
{
    private readonly IJSRuntime _js;
    private readonly IJSInProcessRuntime _jsInProcess;

    public StorageService(IJSRuntime js)
    {
        _js = js;
        _jsInProcess = (IJSInProcessRuntime)_js;
    }

    public async Task<T?> GetItemAsync<T>(string key)
    {
        var json = await _js.InvokeAsync<string>(
            "blazorTodo.getStorage",
            key);

        return string.IsNullOrEmpty(json)
                ? default
                : JsonSerializer.Deserialize<T>(json);
    }

    public async Task SetItemAsync<T>(string key, T item)
    {
        await _js.InvokeVoidAsync(
            "blazorTodo.setStorage",
            key,
            JsonSerializer.Serialize(item));
    }

    public void SetItem<T>(string key, T item)
    {
        _jsInProcess.InvokeVoid(
            "blazorTodo.setStorage",
            key,
            JsonSerializer.Serialize(item));
    }
}
