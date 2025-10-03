using ToDoList.Domain;

namespace ToDoList.Services.Interfaces;

public interface ITodoRepository
{
  public Task CreateTodoListAsync(TodoListModel todoListModel);
  public Task<IEnumerable<TodoListModel>> GetAllTodoListsAsync();
  public Task<TodoListModel?> GetTodoListByIdAsync(int todoListId);
  public Task UpdateTodoListAsync(TodoListModel todoListModel);
  public Task DeleteTodoListAsync(TodoListModel todoListModel);
}
