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

  public async Task<TodoListModel?> UpdateTodoListAsync(TodoListModel todoListModel)
  {
    _logger.LogInformation("TodoService: Updating todo list");
    var model = await _repository.GetTodoListByIdAsync(todoListModel.Id);

    if (model is not null)
    {
      model.Title = todoListModel.Title;
      model.Description = todoListModel.Description;
      await _repository.UpdateTodoListAsync(model);
      return model;
    }
    else
    {
      _logger.LogWarning("TodoService: To be able to update a model, it must exist in the database");
      return null;
    }
  }

  public async Task<TodoListModel?> DeleteTodoListAsync(int id)
  {
    _logger.LogInformation("TodoService: Updating todo list");

    var model = await _repository.GetTodoListByIdAsync(id);

    if (model is not null)
    {
      await _repository.DeleteTodoListAsync(model);
      return model;
    }
    else
    {
      _logger.LogWarning("TodoService: To be able to delete a model, it must exist in the database");
      return null;
    }
  }

  public async Task<IEnumerable<TodoModel>?> GetAllTodoAsync(int todoListId)
  {
    _logger.LogInformation("TodoService: Retrieving todos");
    var model = await _repository.GetTodoListByIdAsync(todoListId);
    if (model is null)
    {
      _logger.LogWarning("TodoService: TodoListModel with id {todoListId} does not exist in database", todoListId);
      return null;
    }
    else
      return model.Todos;
  }
}

