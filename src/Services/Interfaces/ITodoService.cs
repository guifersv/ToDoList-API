using ToDoList.Domain;

namespace ToDoList.Services.Interfaces;

public interface ITodoService
{
  public Task CreateTodoListAsync(TodoListModel todoListModel);
  public Task<IEnumerable<TodoListModel>> GetAllTodoListsAsync();
  public Task<TodoListModel?> GetTodoListByIdAsync(int id);
  public Task UpdateTodoListAsync(TodoListModel todoListModel);
  public Task DeleteTodoListAsync(TodoListModel todoListModel);
}

