using ToDoList.Domain;

namespace ToDoList.Services.Interfaces;

public interface ITodoService
{
  public Task CreateTodoListAsync(TodoListModel todoListModel);
  public Task<IEnumerable<TodoListModel>> GetAllTodoListsAsync();
  public Task<TodoListModel?> GetTodoListByIdAsync(int id);
  public Task<TodoListModel?> UpdateTodoListAsync(TodoListModel todoListModel);
  public Task<TodoListModel?> DeleteTodoListAsync(int id);
  public Task<IEnumerable<TodoModel>?> GetAllTodoAsync(int todoListId);
  public Task<TodoModel?> CreateTodoAsync(int todoListId, TodoDto todoDto);
}

