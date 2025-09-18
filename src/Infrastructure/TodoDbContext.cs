using Microsoft.EntityFrameworkCore;
using ToDoList.Domain;

namespace ToDoList.Infrastructure;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
{
  public DbSet<TodoListModel> TodoLists { get; set; }
}
