namespace BlazorUI.Models;

// TODO: Remove this
public class TodoItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public bool IsComplete { get; set; } = false;

    public string? Notes { get; set; }

    public Priority Priority { get; set; } = Priority.None;

    public DateTime? DueDate { get; set; }
}

// TODO: Remove this
public enum Priority
{
    None = 0,
    Low,
    Medium,
    High
}
