using Microsoft.EntityFrameworkCore;

namespace ToDoList.Domain;

public record TodoModel(string Title, string Description, bool IsCompleted, DateTime DateCreated)
{
  public int Id { get; set; }
  public int TodoListModelId { get; set; }
  public required TodoListModel TodoListModelNavigation { get; set; }
}
