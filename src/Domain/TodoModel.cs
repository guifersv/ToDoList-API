using System.ComponentModel.DataAnnotations;

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
