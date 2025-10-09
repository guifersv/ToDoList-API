using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ToDoList.Domain;

public class TodoListModel
{
    public int Id { get; set; }
    [StringLength(20)]
    public required string Title { get; set; }
    [StringLength(100)]
    public string? Description { get; set; }
    public IEnumerable<TodoModel> Todos { get; set; } = new List<TodoModel>();
}

public record TodoListDto
{
    [BindNever]
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    [BindNever]
    public IEnumerable<TodoDto> Todos { get; set; } = new List<TodoDto>();
}