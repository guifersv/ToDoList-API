using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain;

public class TodoListModel
{
  public int Id { get; set; }
  [Required]
  public required string Title { get; set; }
  public string? Description { get; set; }
  public IEnumerable<TodoModel> Todos { get; set; } = new List<TodoModel>();
}
