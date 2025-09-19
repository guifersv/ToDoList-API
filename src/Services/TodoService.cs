using ToDoList.Infrastructure;
using ToDoList.Services.Interfaces;
using ToDoList.Domain;

namespace ToDoList.Services;

public class TodoService(ITodoRepository repository) : ITodoService
{
  private readonly ITodoRepository _repository = repository;

  public async Task CreateTodoListAsync(TodoListModel todoListModel)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<TodoListModel>> GetAllTodoListsAsync()
  {
    throw new NotImplementedException();
  }

  public async Task<TodoListModel> GetTodoListByIdAsync(int id)
  {
    throw new NotImplementedException();
  }

  public async Task UpdateTodoListAsync(TodoListModel todoListModel)
  {
    throw new NotImplementedException();
  }

  public async Task DeleteTodoListAsync(TodoListModel todoListModel)
  {
    throw new NotImplementedException();
  }
}

