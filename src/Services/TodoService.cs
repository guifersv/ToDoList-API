using ToDoList.Services.Interfaces;
using ToDoList.Utilities;
using ToDoList.Domain;

namespace ToDoList.Services;

public class TodoService(ITodoRepository repository, ILogger<TodoService> logger) : ITodoService
{
  private readonly ILogger<TodoService> _logger = logger;
  private readonly ITodoRepository _repository = repository;

  public async Task<TodoListDto> CreateTodoListAsync(TodoListDto todoListDto)
  {
    _logger.LogInformation("TodoService: Creating the todo list");
    TodoListModel todoList = new()
    {
      Title = todoListDto.Title,
      Description = todoListDto.Description,
    };
    var createdModel = await _repository.CreateTodoListAsync(todoList);

    return Utils.TodoList2Dto(createdModel);
  }

  public async Task<IEnumerable<TodoListDto>> GetAllTodoListsAsync()
  {
    _logger.LogInformation("TodoService: Retrieving all todo lists");
    var todoListModels = await _repository.GetAllTodoListsAsync();

    var todoListDtos = todoListModels
      .Select(Utils.TodoList2Dto).ToList();
    return todoListDtos;
  }

  public async Task<TodoListDto?> GetTodoListByIdAsync(int todoListId)
  {
    _logger.LogInformation("TodoService: Retrieving todo list by id");
    var result = await _repository.GetTodoListByIdAsync(todoListId);

    if (result is not null)
    {
      return Utils.TodoList2Dto(result);
    }
    else
    {
      _logger.LogWarning("TodoService: TodoListModel with id {id} does not exist in database", todoListId);
      return null;
    }
  }

  public async Task<TodoListDto?> UpdateTodoListAsync(int todoListId, TodoListDto todoListDto)
  {
    _logger.LogInformation("TodoService: Updating todo list");
    var model = await _repository.GetTodoListByIdAsync(todoListId);

    if (model is not null)
    {
      model.Title = todoListDto.Title;
      model.Description = todoListDto.Description;
      await _repository.UpdateTodoListAsync(model);

      return Utils.TodoList2Dto(model);
    }
    else
    {
      _logger.LogWarning("TodoService: TodoListModel with id {id} does not exist in database", todoListId);
      return null;
    }
  }

  public async Task<TodoListDto?> DeleteTodoListAsync(int todoListId)
  {
    _logger.LogInformation("TodoService: Updating todo list");

    var model = await _repository.GetTodoListByIdAsync(todoListId);

    if (model is not null)
    {
      await _repository.DeleteTodoListAsync(model);
      return Utils.TodoList2Dto(model);
    }
    else
    {
      _logger.LogWarning("TodoService: TodoListModel with id {id} does not exist in database", todoListId);
      return null;
    }
  }

  public async Task<TodoDto?> CreateTodoAsync(int todoListId, TodoDto todoDto)
  {
    _logger.LogInformation("TodoService: Creating todo");

    var todoList = await _repository.GetTodoListByIdAsync(todoListId);

    if (todoList is not null)
    {
      TodoModel todoModel = new()
      {
        Title = todoDto.Title,
        Description = todoDto.Description,
        DateCreated = todoDto.DateCreated,
        IsCompleted = todoDto.IsCompleted,
        TodoListModelNavigation = todoList,
      };
      ((List<TodoModel>)todoList.Todos).Add(todoModel);
      await _repository.UpdateTodoListAsync(todoList);
      return todoDto;
    }
    else
      _logger.LogWarning("TodoService: TodoListModel with id {id} does not exist in database", todoListId);
    return null;
  }
}
