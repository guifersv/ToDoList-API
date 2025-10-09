using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ToDoList.Domain;

public class TodoModel
{
    public int Id { get; set; }
    [StringLength(20)]
    public required string Title { get; init; }
    [StringLength(100)]
    public string? Description { get; init; }
    public DateTime DateCreated { get; init; }
    public bool IsCompleted { get; set; }
    public int TodoListModelId { get; set; }
    public required TodoListModel TodoListModelNavigation { get; set; }
}

public record TodoDto
{
    [BindNever]
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime DateCreated { get; set; }
    public bool IsCompleted { get; set; }
}