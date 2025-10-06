using ToDoList.Domain;

namespace ToDoList.Services.Interfaces;

public interface ITodoService
{
  public Task<TodoListDto> CreateTodoListAsync(TodoListDto todoListDto);
  public Task<IEnumerable<TodoListDto>> GetAllTodoListsAsync();
  public Task<TodoListDto?> GetTodoListByIdAsync(int todoListId);
  public Task<TodoListDto?> UpdateTodoListAsync(int todoListId, TodoListDto todoListDto);
  public Task<TodoListDto?> DeleteTodoListAsync(int todoListId);
}

