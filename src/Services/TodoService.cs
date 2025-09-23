using ToDoList.Services.Interfaces;
using ToDoList.Domain;

namespace ToDoList.Services;

public class TodoService(ITodoRepository repository, ILogger<TodoService> logger) : ITodoService
{
  private readonly ILogger<TodoService> _logger = logger;
  private readonly ITodoRepository _repository = repository;

  public async Task CreateTodoListAsync(TodoListModel todoListModel)
  {
    _logger.LogInformation("TodoService: Creating a todo list");
    await _repository.CreateTodoListAsync(todoListModel);
  }

  public async Task<IEnumerable<TodoListModel>> GetAllTodoListsAsync()
  {
    _logger.LogInformation("TodoService: Retrieving all todo lists");
    return await _repository.GetAllTodoListsAsync();
  }

  public async Task<TodoListModel?> GetTodoListByIdAsync(int id)
  {
    _logger.LogInformation("TodoService: Retrieving todo list by id");
    var result = await _repository.GetTodoListByIdAsync(id);
    if (result is null)
      _logger.LogWarning("TodoService: The model doesn't exist");
    return result;
  }

  public async Task UpdateTodoListAsync(TodoListModel todoListModel)
  {
    _logger.LogInformation("TodoService: Updating todo list");

    if (await _repository.GetTodoListByIdAsync(todoListModel.Id) is not null)
      await _repository.UpdateTodoListAsync(todoListModel);
    else
      _logger.LogWarning("TodoService: To be able to update a model, it must exist in the database");
  }

  public async Task DeleteTodoListAsync(TodoListModel todoListModel)
  {
    _logger.LogInformation("TodoService: Updating todo list");

    if (await _repository.GetTodoListByIdAsync(todoListModel.Id) is not null)
      await _repository.DeleteTodoListAsync(todoListModel);
    else
      _logger.LogWarning("TodoService: To be able to delete a model, it must exist in the database");
  }
}

