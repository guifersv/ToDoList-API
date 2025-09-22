using ToDoList.Services.Interfaces;
using ToDoList.Domain;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Infrastructure;

public class TodoRepository(TodoDbContext context) : ITodoRepository
{
  private readonly TodoDbContext _context = context;

  public async Task CreateTodoListAsync(TodoListModel todoListModel)
  {
    await _context.TodoLists.AddAsync(todoListModel);
    await _context.SaveChangesAsync();
  }

  public async Task<IEnumerable<TodoListModel>> GetAllTodoListsAsync()
  {
    return await _context.TodoLists.AsNoTracking().ToListAsync();
  }

  public async Task<TodoListModel?> GetTodoListByIdAsync(int id)
  {
    return await _context.TodoLists.FindAsync(id);
  }

  public async Task UpdateTodoListAsync(TodoListModel todoListModel)
  {
    _context.TodoLists.Update(todoListModel);
    await _context.SaveChangesAsync();
  }

  public async Task DeleteTodoListAsync(TodoListModel todoListModel)
  {
    _context.TodoLists.Remove(todoListModel);
    await _context.SaveChangesAsync();
  }
}
