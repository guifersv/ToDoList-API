using ToDoList.Services.Interfaces;
using ToDoList.Domain;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Infrastructure;

public class TodoRepository(TodoDbContext context) : ITodoRepository
{
  private readonly TodoDbContext _context = context;

  public async Task<TodoListModel> CreateTodoListAsync(TodoListModel todoListModel)
  {
    var createdModel = await _context.TodoLists.AddAsync(todoListModel);
    await _context.SaveChangesAsync();
    return createdModel.Entity;
  }

  public async Task<IEnumerable<TodoListModel>> GetAllTodoListsAsync()
  {
    var models = await _context.TodoLists.Include(
        t => t.Todos).ToListAsync();
    return models;
  }

  public async Task<TodoListModel?> GetTodoListByIdAsync(int todoListId)
  {
    var model = await _context.TodoLists.Include(
        t => t.Todos).FirstOrDefaultAsync(m => m.Id == todoListId);
    return model;
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
