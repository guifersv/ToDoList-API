using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain;

public class TaskList
{
  public int Id { get; set; }
  [Required]
  public required string Title { get; set; }
  public string? Description { get; set; }
  public IEnumerable<Task> Tasks { get; set; } = new List<Task>();
}
