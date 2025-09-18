using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain;

public class TodoListModel
{
  public int Id { get; set; }
  [Required]
  [StringLength(100)]
  public required string Title { get; set; }
  [StringLength(100)]
  public string? Description { get; set; }
  public IEnumerable<TodoModel> Todos { get; set; } = new List<TodoModel>();
}
