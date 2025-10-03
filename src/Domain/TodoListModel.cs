using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ToDoList.Domain;

public class TodoListModel
{
  [BindNever]
  public int Id { get; set; }
  [StringLength(20)]
  public required string Title { get; set; }
  [StringLength(100)]
  public string? Description { get; set; }
  public IEnumerable<TodoModel> Todos { get; set; } = [];
}

public record TodoListDto
{
  public int Id { get; }
  public string? Title { get; set; }
  public string? Description { get; set; }
  public IEnumerable<TodoModel> Todos { get; } = [];
}
