using ToDoList.Services.Interfaces;
using ToDoList.Domain;

namespace ToDoList.Infrastructure;

public class TodoRepository(TodoDbContext context) : ITodoRepository
{
  private readonly TodoDbContext _context = context;

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
